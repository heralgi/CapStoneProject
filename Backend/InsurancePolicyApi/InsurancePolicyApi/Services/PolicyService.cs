using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPlanRepository _planRepository;

        public PolicyService(
            IPolicyRepository policyRepository,
            ICustomerRepository customerRepository,
            IPlanRepository planRepository)
        {
            _policyRepository = policyRepository;
            _customerRepository = customerRepository;
            _planRepository = planRepository;
        }

        public async Task<Policy> PurchasePolicyAsync(int customerId, CustomerPolicyPurchaseRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                throw new Exception("Customer not found.");

            var plan = await _planRepository.GetByIdAsync(request.PlanId);

            if (plan == null)
                throw new Exception("Policy plan not found.");

            if (!plan.IsActive)
                throw new Exception("Policy plan is inactive.");

            var policy = new Policy
            {
                CustomerId = customerId,
                PolicyPlanId = request.PlanId,
                PolicyNumber = GeneratePolicyNumber(),
                PolicyStatus = PolicyStatus.PendingPayment,
                StartDate = request.StartDate,
                EndDate = request.StartDate.AddYears(plan.DurationYears), // adjust according to your entity
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return await _policyRepository.PurchasePolicyAsync(policy);
        }
        private static string GeneratePolicyNumber()
        {
            return $"POL{Guid.NewGuid().ToString("N")[..10].ToUpper()}";
        }
        public async Task<Policy?> IssuePolicyAsync(int policyId)
        {
            var policy = await _policyRepository.GetByIdAsync(policyId);

            if (policy == null)
                return null;

            policy.PolicyStatus = PolicyStatus.Active;
            policy.StartDate = DateTime.UtcNow;

            return await _policyRepository.UpdateAsync(policy);
        }

        public async Task<Policy?> GetByPolicyNumberAsync(string policyNumber)
        {
            return await _policyRepository.GetByPolicyNumberAsync(policyNumber);
        }

        public async Task<IEnumerable<Policy>> GetByCustomerIdAsync(int customerId)
        {
            return await _policyRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Policy>> GetPoliciesAsync()
        {
            return await _policyRepository.GetPoliciesAsync();
        }
    }
}
