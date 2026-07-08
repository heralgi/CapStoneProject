using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<InsuranceProduct>> GetAllAsync();

        Task<InsuranceProduct?> GetByIdAsync(int id);

        Task<InsuranceProduct> AddAsync(InsuranceProduct product);

        Task<InsuranceProduct> UpdateAsync(InsuranceProduct product);

        Task<bool> DeactivateAsync(int id);
    }
}
