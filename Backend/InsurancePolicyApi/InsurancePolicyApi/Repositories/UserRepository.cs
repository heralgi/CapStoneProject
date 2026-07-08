using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _ctx.Users
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _ctx.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _ctx.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _ctx.Users.Update(user);

            await _ctx.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            var user = await _ctx.Users.FindAsync(id);

            if (user == null)
                return false;

            user.IsActive = true;

            await _ctx.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var user = await _ctx.Users.FindAsync(id);

            if (user == null)
                return false;

            user.IsActive = false;

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
