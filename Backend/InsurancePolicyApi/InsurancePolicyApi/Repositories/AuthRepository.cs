using InsurancePolicyApi.Data;
using InsurancePolicyApi.DTOs.Auth;
using InsurancePolicyApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly AppDbContext _ctx;
        private readonly PasswordHasher<User> _passwordHasher;
        public AuthRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _passwordHasher = new PasswordHasher<User>();
        }
        public LoginResponse Login(LoginRequest req)
        {
            LoginResponse res = new LoginResponse { isSuccess = false };

            var user = _ctx.Users.FirstOrDefault(r => r.Email == req.Email);
            
            if(user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(
                         user,
                         user.PasswordHash,
                         req.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    res = new LoginResponse
                    {
                        isSuccess = true,
                        UserName = user.FullName,
                        Email = user.Email,
                        UserId = user.Id,
                        Token = "",
                        Role = user.Role.ToString()
                    };
                }
            }
            return res;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterCustomerAsync(User user, Customer customer)
        {
            await using var transaction = await _ctx.Database.BeginTransactionAsync();

            await _ctx.Users.AddAsync(user);

            await _ctx.SaveChangesAsync();

            customer.UserId = user.Id;

            await _ctx.Customers.AddAsync(customer);

            await _ctx.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _ctx.Users
                .AnyAsync(x => x.Email == email);
        }
    }
}
