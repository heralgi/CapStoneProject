using InsurancePolicyApi.Data;
using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _ctx;

        public CustomerRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PagedResponse<Customer>> GetAllAsync(PageQuery pagequery)
        {
            var query = _ctx.Customers.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((pagequery.PageNumber - 1) * pagequery.PageSize)
                .Take(pagequery.PageSize)
                .ToListAsync();
            return new PagedResponse<Customer>
            {
                Records = items,
                CurrentPage = pagequery.PageNumber,
                PageSize = pagequery.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagequery.PageSize)
            };
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _ctx.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetByUserIdAsync(int userId)
        {
            return await _ctx.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Customer> CreateProfileAsync(Customer customer)
        {
            await _ctx.Customers.AddAsync(customer);

            await _ctx.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateProfileAsync(Customer customer)
        {
            _ctx.Customers.Update(customer);

            await _ctx.SaveChangesAsync();

            return customer;
        }
    }
}
