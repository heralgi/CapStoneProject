using InsurancePolicyApi.DTOs.Product;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<InsuranceProduct>> GetAllAsync();
        Task<IEnumerable<InsuranceProduct>> GetAllActiveAsync();

        Task<InsuranceProduct?> GetByIdAsync(int id);

        Task<InsuranceProduct> AddAsync(ProductRequest request);

        Task<InsuranceProduct?> UpdateAsync(int id, InsuranceProduct product);

        Task<bool> DeactivateAsync(int id);
    }
}
