using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<InsuranceProduct>> GetAllAsync();

        Task<InsuranceProduct?> GetByIdAsync(int id);

        Task<InsuranceProduct> AddAsync(InsuranceProduct product);

        Task<InsuranceProduct?> UpdateAsync(int id, InsuranceProduct product);

        Task<bool> DeactivateAsync(int id);
    }
}
