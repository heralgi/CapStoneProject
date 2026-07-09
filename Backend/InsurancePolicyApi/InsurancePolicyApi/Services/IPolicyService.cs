using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IPolicyService
    {
        Task<Policy> PurchasePolicyAsync(int customerId, CustomerPolicyPurchaseRequest request);

        Task<Policy?> IssuePolicyAsync(int policyId);

        Task<Policy?> GetByPolicyNumberAsync(string policyNumber);

        Task<IEnumerable<Policy>> GetByCustomerIdAsync(int customerId);

        Task<IEnumerable<Policy>> GetPoliciesAsync(int userId);
    }
}
