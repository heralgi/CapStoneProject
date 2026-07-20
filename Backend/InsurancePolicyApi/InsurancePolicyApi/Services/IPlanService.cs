using InsurancePolicyApi.DTOs.Plan;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanResponse>> GetByProductIdAsync(int productId);

        Task<PlanResponse> AddAsync(PlanRequest plan);

        Task<PolicyPlan?> UpdateAsync(int id, PlanRequest plan);

        Task<bool> DeactivateAsync(int id);
    }
}
