using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Data
{
    /// <summary>
    /// Applies pending migrations and seeds the initial Admin account.
    /// Admin and agent accounts are created internally, never by public registration (ASM-004, USR-BR-004).
    /// Admin credentials are read from configuration ("Seed:Admin:*") with safe defaults for development.
    /// </summary>
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext db, IConfiguration config, ILogger logger)
        {
            await db.Database.MigrateAsync();

            var adminEmail = config["Seed:Admin:Email"] ?? "admin@insurance.com";

            if (await db.Users.AnyAsync(u => u.Email == adminEmail))
            {
                return; // already seeded
            }

            var now = DateTime.UtcNow;
            var admin = new User
            {
                FullName = config["Seed:Admin:FullName"] ?? "System Administrator",
                Email = adminEmail,
                MobileNumber = config["Seed:Admin:MobileNumber"] ?? "9999999999",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedDate = now,
                UpdatedDate = now
            };

            var rawPassword = config["Seed:Admin:Password"] ?? "Admin@12345";
            admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, rawPassword);

            await db.Users.AddAsync(admin);
            await db.SaveChangesAsync();

            logger.LogInformation("Seeded initial Admin account: {Email}", adminEmail);
        }
    }
}
