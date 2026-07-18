using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IPolicyService
    {
        Task<PolicyResponse> PurchasePolicyAsync(int customerId, CustomerPolicyPurchaseRequest request);

        Task<PolicyResponse?> IssuePolicyAsync(int policyId);

        Task<PolicyResponse?> GetByPolicyNumberAsync(string policyNumber);

        Task<IEnumerable<PolicyResponse>> GetByCustomerIdAsync(int customerId);

        Task<PagedResponse<Policy>> GetPoliciesAsync(int userId, PageQuery pq);
    }
}
