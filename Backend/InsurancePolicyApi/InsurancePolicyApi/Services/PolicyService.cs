using InsurancePolicyApi.DTOs.Common;
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

        public async Task<PolicyResponse> PurchasePolicyAsync(int userId, CustomerPolicyPurchaseRequest request)
        {
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null)
                throw new Exception("Customer not found.");

            var plan = await _planRepository.GetByIdAsync(request.PlanId);

            if (plan == null)
                throw new Exception("Policy plan not found.");

            if (!plan.IsActive)
                throw new Exception("Policy plan is inActive.");

            var policies = await _policyRepository.GetPoliciesAsync(userId);

            // 1. Check if the customer already has a policy with this PlanId
            if (policies.Any(p => p.PolicyPlanId == request.PlanId && 
                    (p.PolicyStatus == PolicyStatus.PendingPayment || p.PolicyStatus == PolicyStatus.Active))){
                throw new Exception("Policy Plan Already Purchased.");
            }
            
            var policy = new Policy();

            if (plan.InsuranceProduct.ProductType == ProductType.Motor)
            {
                policy = new Policy
                {
                    CustomerId = customer.Id,
                    PolicyPlanId = request.PlanId,
                    PolicyNumber = GeneratePolicyNumber(),
                    PolicyStatus = PolicyStatus.PendingPayment,
                    VehicleNumber = request.Identifier,
                    AadharNumber = "-",
                    StartDate = request.StartDate,
                    EndDate = request.StartDate.AddYears(plan.DurationYears), // adjust according to your entity
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
            }
            else
            {
                policy = new Policy
                {
                    CustomerId = customer.Id,
                    PolicyPlanId = request.PlanId,
                    PolicyNumber = GeneratePolicyNumber(),
                    PolicyStatus = PolicyStatus.PendingPayment,
                    VehicleNumber = "-",
                    AadharNumber = request.Identifier,
                    StartDate = request.StartDate,
                    EndDate = request.StartDate.AddYears(plan.DurationYears), // adjust according to your entity
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
            }

            var response = await _policyRepository.PurchasePolicyAsync(policy);
            PolicyResponse resDto = new PolicyResponse()
            {
                PolicyId = response.Id,
                PolicyNumber = response.PolicyNumber,
                CustomerId = response.CustomerId,
                PlanName = response.PolicyPlan.PlanName,
                CoverageAmount = response.PolicyPlan.CoverageAmount,
                PremiumAmount = response.PolicyPlan.PremiumAmount,
                TotalPremiumPaid = response.TotalPremiumPaid,
                PremiumType = response.PolicyPlan.PremiumType.ToString(),
                PolicyStatus = response.PolicyStatus.ToString(),
                StartDate = response.StartDate,
                EndDate = response.EndDate
            };
            return resDto;
        }
        private static string GeneratePolicyNumber()
        {
            return $"POL{Guid.NewGuid().ToString("N")[..10].ToUpper()}";
        }
        public async Task<PolicyResponse?> IssuePolicyAsync(int policyId)
        {
            var policy = await _policyRepository.GetByIdAsync(policyId);

            if (policy == null)
                return null;

            policy.PolicyStatus = PolicyStatus.Active;
            policy.StartDate = DateTime.UtcNow;

            var response = await _policyRepository.UpdateAsync(policy);
            PolicyResponse resDto = new PolicyResponse()
            {
                PolicyId = response.Id,
                PolicyNumber = response.PolicyNumber,
                CustomerId = response.CustomerId,
                PlanName = response.PolicyPlan.PlanName,
                CoverageAmount = response.PolicyPlan.CoverageAmount,
                PremiumAmount = response.PolicyPlan.PremiumAmount,
                TotalPremiumPaid = response.TotalPremiumPaid,
                PremiumType = response.PolicyPlan.PremiumType.ToString(),
                PolicyStatus = response.PolicyStatus.ToString(),
                StartDate = response.StartDate,
                EndDate = response.EndDate
            };
            return resDto;
        }

        public async Task<PolicyResponse?> GetByPolicyNumberAsync(string policyNumber)
        {
            var response = await _policyRepository.GetByPolicyNumberAsync(policyNumber);
            PolicyResponse resDto = new PolicyResponse()
            {
                PolicyId = response.Id,
                PolicyNumber = response.PolicyNumber,
                CustomerId = response.CustomerId,
                PlanName = response.PolicyPlan.PlanName,
                CoverageAmount = response.PolicyPlan.CoverageAmount,
                PremiumAmount = response.PolicyPlan.PremiumAmount,
                TotalPremiumPaid = response.TotalPremiumPaid,
                PremiumType = response.PolicyPlan.PremiumType.ToString(),
                PolicyStatus = response.PolicyStatus.ToString(),
                StartDate = response.StartDate,
                EndDate = response.EndDate
            };
            return resDto;
        }

        public async Task<IEnumerable<PolicyResponse>> GetByCustomerIdAsync(int customerId)
        {
            var responses = await _policyRepository.GetByCustomerIdAsync(customerId);
            var listresDto = new List<PolicyResponse>();

            foreach (Policy response in responses)
            {
                listresDto.Add(new PolicyResponse()
                {
                    PolicyId = response.Id,
                    PolicyNumber = response.PolicyNumber,
                    CustomerId = response.CustomerId,
                    PlanName = response.PolicyPlan.PlanName,
                    CoverageAmount = response.PolicyPlan.CoverageAmount,
                    PremiumAmount = response.PolicyPlan.PremiumAmount,
                    TotalPremiumPaid = response.TotalPremiumPaid,
                    PremiumType = response.PolicyPlan.PremiumType.ToString(),
                    PolicyStatus = response.PolicyStatus.ToString(),
                    StartDate = response.StartDate,
                    EndDate = response.EndDate
                });
            }
            return listresDto;
        }

        public async Task<PagedResponse<Policy>> GetPoliciesAsync(int userId, PageQuery pq)
        {
            var policies = await _policyRepository.GetPoliciesAsync(userId, pq);
            return policies;



            /*var policyResponses = new List<PolicyResponse>();
            foreach(Policy policy in policies)
            {
                policyResponses.Add(new PolicyResponse()
                {
                    PolicyId = policy.Id,
                    PolicyNumber = policy.PolicyNumber,
                    CustomerId = policy.CustomerId,
                    PlanName = policy.PolicyPlan.PlanName,
                    CoverageAmount = policy.PolicyPlan.CoverageAmount,
                    PremiumAmount = policy.PolicyPlan.PremiumAmount,
                    TotalPremiumPaid = policy.TotalPremiumPaid,
                    PremiumType = policy.PolicyPlan.PremiumType.ToString(),
                    PolicyStatus = policy.PolicyStatus.ToString(),
                    StartDate = policy.StartDate,
                    EndDate = policy.EndDate
                });
            }

            return policyResponses;*/
        }

        public async Task<IEnumerable<PolicyResponse>> GetPoliciesAsync(int userId)
        {
            var policies = await _policyRepository.GetPoliciesAsync(userId);
            List<PolicyResponse> policyResponses = new List<PolicyResponse>();

            foreach (Policy policy in policies)
            {
                policyResponses.Add(
                    new PolicyResponse()
                    {
                        PolicyId = policy.Id,
                        PolicyNumber = policy.PolicyNumber,
                        CustomerId = policy.CustomerId,
                        PlanName = policy.PolicyPlan.PlanName,
                        CoverageAmount = policy.PolicyPlan.CoverageAmount,
                        PremiumAmount = policy.PolicyPlan.PremiumAmount,
                        PremiumType = policy.PolicyPlan.PremiumType.ToString(),
                        StartDate = policy.StartDate,
                        EndDate = policy.EndDate,
                        PolicyStatus = policy.PolicyStatus.ToString(),
                        AadharNumber = policy.AadharNumber,
                        VehicleNumber = policy.VehicleNumber,
                        TotalPremiumPaid = policy.TotalPremiumPaid
                    }
                );
            }

            return policyResponses;
        }

        public async Task<IEnumerable<PolicyResponse>> GetAllPoliciesAsync()
        {
            var policies = await _policyRepository.GetAllPoliciesAsync();
            List<PolicyResponse> policyResponses = new List<PolicyResponse>();

            foreach(Policy policy in policies)
            {
                policyResponses.Add(
                    new PolicyResponse()
                    {
                        PolicyId = policy.Id,
                        PolicyNumber = policy.PolicyNumber,
                        CustomerId = policy.CustomerId,
                        PlanName = policy.PolicyPlan.PlanName,
                        CoverageAmount = policy.PolicyPlan.CoverageAmount,
                        PremiumAmount = policy.PolicyPlan.PremiumAmount,
                        PremiumType = policy.PolicyPlan.PremiumType.ToString(),
                        StartDate = policy.StartDate,
                        EndDate = policy.EndDate,
                        PolicyStatus = policy.PolicyStatus.ToString(),
                        AadharNumber = policy.AadharNumber,
                        VehicleNumber = policy.VehicleNumber,
                        TotalPremiumPaid = policy.TotalPremiumPaid
                    }
                );
            }

            return policyResponses;
        }
    }
}
