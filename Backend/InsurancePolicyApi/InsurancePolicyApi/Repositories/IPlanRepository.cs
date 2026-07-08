using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IPlanRepository
    {
        Task<IEnumerable<PolicyPlan>> GetByProductIdAsync(int productId);

        Task<PolicyPlan> AddAsync(PolicyPlan plan);

        Task<PolicyPlan> UpdateAsync(PolicyPlan plan);

        Task<bool> DeactivateAsync(int id);

        Task<PolicyPlan?> GetByIdAsync(int id);
    }
}
