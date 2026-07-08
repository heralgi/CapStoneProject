using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IProductRepository
    {
        Task<bool> ExistsByNameAsync(string productName);
        Task<IEnumerable<InsuranceProduct>> GetAllAsync();
        Task<IEnumerable<InsuranceProduct>> GetActiveAsync();

        Task<InsuranceProduct?> GetByIdAsync(int id);

        Task<InsuranceProduct> AddAsync(InsuranceProduct product);

        Task<InsuranceProduct> UpdateAsync(InsuranceProduct product);

        Task<bool> DeactivateAsync(int id);
    }
}
