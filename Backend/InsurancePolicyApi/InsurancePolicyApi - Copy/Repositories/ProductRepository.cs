using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _ctx;

        public ProductRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<InsuranceProduct>> GetAllAsync()
        {
            return await _ctx.InsuranceProducts.ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string productName)
        {
            return await _ctx.InsuranceProducts
                .AnyAsync(p => p.ProductName == productName);
        }

        public async Task<InsuranceProduct?> GetByIdAsync(int id)
        {
            return await _ctx.InsuranceProducts
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<InsuranceProduct>> GetActiveAsync()
        {
            return await _ctx.InsuranceProducts
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<InsuranceProduct> AddAsync(InsuranceProduct product)
        {
            await _ctx.InsuranceProducts.AddAsync(product);

            await _ctx.SaveChangesAsync();

            return product;
        }

        public async Task<InsuranceProduct> UpdateAsync(InsuranceProduct product)
        {
            _ctx.InsuranceProducts.Update(product);

            await _ctx.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var product = await _ctx.InsuranceProducts.FindAsync(id);

            if (product == null)
                return false;

            product.IsActive = false;

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
