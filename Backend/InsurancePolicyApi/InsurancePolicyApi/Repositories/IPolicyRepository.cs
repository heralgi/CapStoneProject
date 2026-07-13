using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IPolicyRepository
    {
        Task<Policy> PurchasePolicyAsync(Policy policy);

        Task<Policy?> IssuePolicyAsync(Policy policy);

        Task<Policy?> GetByPolicyNumberAsync(string policyNumber);

        Task<IEnumerable<Policy>> GetByCustomerIdAsync(int customerId);

        Task<PagedResponse<Policy>> GetPoliciesAsync(int userId, PageQuery pq);
        Task<IEnumerable<Policy>> GetPoliciesAsync(int userId);

        Task<Policy?> GetByIdAsync(int id);

        Task<Policy> UpdateAsync(Policy policy);
    }
}
