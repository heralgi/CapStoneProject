using InsurancePolicyApi.Data;
using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.DTOs.Common;
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

        public async Task<PagedResponse<User>> GetAllAsync(PageQuery pagequery)
        {
            var query = _ctx.Users.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((pagequery.PageNumber - 1) * pagequery.PageSize)
                .Take(pagequery.PageSize)
                .ToListAsync();
            return new PagedResponse<User>
            {
                Records = items,
                CurrentPage = pagequery.PageNumber,
                PageSize = pagequery.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagequery.PageSize)
            };
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
