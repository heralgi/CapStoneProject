using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InsuranceProduct>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<InsuranceProduct?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<InsuranceProduct> AddAsync(InsuranceProduct product)
        {
            product.IsActive = true;

            return await _repository.AddAsync(product);
        }

        public async Task<InsuranceProduct?> UpdateAsync(int id, InsuranceProduct product)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.ProductName = product.ProductName;
            existing.Description = product.Description;
            existing.ProductType = product.ProductType;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            if (!product.IsActive)
                return true;

            return await _repository.DeactivateAsync(id);
        }
    }
}
