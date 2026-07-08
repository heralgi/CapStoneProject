using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IPlanService
    {
        Task<IEnumerable<PolicyPlan>> GetByProductIdAsync(int productId);

        Task<PolicyPlan> AddAsync(PolicyPlan plan);

        Task<PolicyPlan?> UpdateAsync(int id, PolicyPlan plan);

        Task<bool> DeactivateAsync(int id);
    }
}
