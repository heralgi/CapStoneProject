using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Data
{
    /// <summary>
    /// EF Core database context for the Insurance Policy and Claim Management System.
    /// Relationships and constraints follow the SRS Entity Relationship Mapping (§10) and
    /// the no-hard-delete data-retention rules (DRD-001..008): deletes are restricted so
    /// business records cannot cascade away.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<InsuranceProduct> InsuranceProducts => Set<InsuranceProduct>();
        public DbSet<PolicyPlan> PolicyPlans => Set<PolicyPlan>();
        public DbSet<Policy> Policies => Set<Policy>();
        public DbSet<PremiumPayment> PremiumPayments => Set<PremiumPayment>();
        public DbSet<Claim> Claims => Set<Claim>();
        public DbSet<ClaimDocument> ClaimDocuments => Set<ClaimDocument>();
        public DbSet<ClaimStatusHistory> ClaimStatusHistories => Set<ClaimStatusHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Store all enums as strings for readable, stable persistence.
            // (applied per-property below where it matters for querying/readability)

            // ---- User (§9.1) ----
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.FullName).IsRequired().HasMaxLength(150);
                e.Property(u => u.Email).IsRequired().HasMaxLength(256);
                e.Property(u => u.PasswordHash).IsRequired();
                e.Property(u => u.MobileNumber).IsRequired().HasMaxLength(20);
                e.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
                e.HasIndex(u => u.Email).IsUnique(); // USR-BR-001
            });

            // ---- Customer (§9.2) — 1:1 with User (MAP-001, MAP-BR-001) ----
            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Address).IsRequired().HasMaxLength(250);
                e.Property(c => c.City).IsRequired().HasMaxLength(100);
                e.Property(c => c.State).IsRequired().HasMaxLength(100);
                e.Property(c => c.PinCode).IsRequired().HasMaxLength(10);
                e.Property(c => c.NomineeName).IsRequired().HasMaxLength(150);
                e.Property(c => c.NomineeRelation).IsRequired().HasMaxLength(50);
                e.HasIndex(c => c.UserId).IsUnique(); // one profile per user (CUS-BR-002)
                e.HasOne(c => c.User)
                    .WithOne(u => u.Customer)
                    .HasForeignKey<Customer>(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---- InsuranceProduct (§9.3) ----
            modelBuilder.Entity<InsuranceProduct>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.ProductName).IsRequired().HasMaxLength(150);
                e.Property(p => p.ProductType).HasConversion<string>().HasMaxLength(20);
                e.Property(p => p.Description).IsRequired().HasMaxLength(1000);
                e.HasIndex(p => p.ProductName).IsUnique(); // PRD-BR-001
            });

            // ---- PolicyPlan (§9.4) — M:1 InsuranceProduct (MAP-002, MAP-BR-002) ----
            modelBuilder.Entity<PolicyPlan>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.PlanName).IsRequired().HasMaxLength(150);
                e.Property(p => p.CoverageAmount).HasColumnType("decimal(18,2)");
                e.Property(p => p.PremiumAmount).HasColumnType("decimal(18,2)");
                e.Property(p => p.PremiumType).HasConversion<string>().HasMaxLength(20);
                e.Property(p => p.TermsAndConditions).IsRequired().HasMaxLength(2000);
                e.HasOne(p => p.InsuranceProduct)
                    .WithMany(pr => pr.Plans)
                    .HasForeignKey(p => p.InsuranceProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---- Policy (§9.5) — M:1 Customer (MAP-004) & PolicyPlan (MAP-003) ----
            modelBuilder.Entity<Policy>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.PolicyNumber).IsRequired().HasMaxLength(40);
                e.Property(p => p.PolicyStatus).HasConversion<string>().HasMaxLength(20);
                e.Property(p => p.TotalPremiumPaid).HasColumnType("decimal(18,2)");
                e.HasIndex(p => p.PolicyNumber).IsUnique(); // POL-BR-003, ID-RUL-001
                e.HasOne(p => p.Customer)
                    .WithMany(c => c.Policies)
                    .HasForeignKey(p => p.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(p => p.PolicyPlan)
                    .WithMany(pl => pl.Policies)
                    .HasForeignKey(p => p.PolicyPlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---- PremiumPayment (§9.6) — M:1 Policy (MAP-005, MAP-BR-004) ----
            modelBuilder.Entity<PremiumPayment>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                e.Property(p => p.PaymentMode).HasConversion<string>().HasMaxLength(20);
                e.Property(p => p.TransactionReference).IsRequired().HasMaxLength(100);
                e.Property(p => p.PaymentStatus).HasConversion<string>().HasMaxLength(20);
                e.HasIndex(p => p.TransactionReference).IsUnique(); // PAY-BR-003, ID-RUL-003
                e.HasOne(p => p.Policy)
                    .WithMany(po => po.Payments)
                    .HasForeignKey(p => p.PolicyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---- Claim (§9.7) — M:1 Policy (MAP-006, MAP-BR-005) ----
            modelBuilder.Entity<Claim>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.ClaimNumber).IsRequired().HasMaxLength(40);
                e.Property(c => c.ClaimAmount).HasColumnType("decimal(18,2)");
                e.Property(c => c.ClaimReason).IsRequired().HasMaxLength(1000);
                e.Property(c => c.ClaimStatus).HasConversion<string>().HasMaxLength(30);
                e.Property(c => c.InternalStaffRemarks).HasMaxLength(1000);
                e.Property(c => c.AdminRemarks).HasMaxLength(1000);
                e.HasIndex(c => c.ClaimNumber).IsUnique(); // CLM-BR-002, ID-RUL-002
                e.HasOne(c => c.Policy)
                    .WithMany(po => po.Claims)
                    .HasForeignKey(c => c.PolicyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---- ClaimDocument (§9.8) — M:1 Claim (MAP-007, MAP-BR-006) ----
            modelBuilder.Entity<ClaimDocument>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.DocumentName).IsRequired().HasMaxLength(200);
                e.Property(d => d.DocumentType).IsRequired().HasMaxLength(100);
                e.Property(d => d.DocumentReference).IsRequired().HasMaxLength(500);
                e.HasOne(d => d.Claim)
                    .WithMany(c => c.Documents)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade); // documents belong to their claim
            });

            // ---- ClaimStatusHistory (§9.9) — M:1 Claim (MAP-008) & User (MAP-009) ----
            modelBuilder.Entity<ClaimStatusHistory>(e =>
            {
                e.HasKey(h => h.Id);
                e.Property(h => h.PreviousStatus).HasConversion<string>().HasMaxLength(30);
                e.Property(h => h.NewStatus).HasConversion<string>().HasMaxLength(30);
                e.Property(h => h.Remarks).HasMaxLength(1000);
                e.HasOne(h => h.Claim)
                    .WithMany(c => c.StatusHistory)
                    .HasForeignKey(h => h.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(h => h.UpdatedByUser)
                    .WithMany(u => u.ClaimStatusUpdates)
                    .HasForeignKey(h => h.UpdatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1004,
                    FullName = "System Administrator",
                    Email = "admin@example.com",
                    MobileNumber = "9999999999",
                    Role = UserRole.Admin,
                    PasswordHash = "AQAAAAIAAYagAAAAEJMRgHPtb2oo4lA055eMUC+sEE1fdQmOhAnlhUBOBB3gmpCAMf3bm3Nf+LOrJgWOaA=="
                }
            );
        }
    }
}
