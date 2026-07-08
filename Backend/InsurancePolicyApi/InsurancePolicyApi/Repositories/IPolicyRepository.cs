using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IPolicyRepository
    {
        Task<Policy> PurchasePolicyAsync(Policy policy);

        Task<Policy?> IssuePolicyAsync(Policy policy);

        Task<Policy?> GetByPolicyNumberAsync(string policyNumber);

        Task<IEnumerable<Policy>> GetByCustomerIdAsync(int customerId);

        Task<IEnumerable<Policy>> GetPoliciesAsync();

        Task<Policy?> GetByIdAsync(int id);

        Task<Policy> UpdateAsync(Policy policy);
    }
}
