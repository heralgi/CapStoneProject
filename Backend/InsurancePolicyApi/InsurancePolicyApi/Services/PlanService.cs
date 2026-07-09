using InsurancePolicyApi.DTOs.Plan;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Repositories;
using System.Numerics;

namespace InsurancePolicyApi.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IProductRepository _productRepository;

        public PlanService(
            IPlanRepository planRepository,
            IProductRepository productRepository)
        {
            _planRepository = planRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<PlanResponse>> GetByProductIdAsync(int productId)
        {
            var plans = await _planRepository.GetByProductIdAsync(productId);

            var listResponse = new List<PlanResponse>();

            foreach (var element in plans)
            {
                listResponse.Add(new PlanResponse
                {
                    PlanId = element.Id,
                    PlanName = element.PlanName,
                    ProductId = element.InsuranceProductId,
                    IsActive = element.IsActive
                });
            }

            return listResponse;
        }

        public async Task<PlanResponse> AddAsync(PlanRequest plan)
        {
            var product = await _productRepository.GetByIdAsync(plan.ProductId);

            if (product == null)
                throw new Exception("Insurance Product does not exist.");

            if (!product.IsActive)
                throw new Exception("Cannot add a plan to an inactive insurance product.");
            if (plan.CoverageAmount <= plan.PremiumAmount)
            {
                throw new Exception("Coverage amount must be higher than premium amount.");
            }

            var NewPlan = new PolicyPlan()
            {
                InsuranceProductId = plan.ProductId,
                PlanName = plan.PlanName,
                CoverageAmount = plan.CoverageAmount,
                PremiumAmount = plan.PremiumAmount,
                IsActive = plan.IsActive,
                TermsAndConditions = plan.TermsAndConditions,
                PremiumType = plan.PremiumType,
                DurationYears = plan.DurationYears,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            plan.IsActive = true;

            var planRes = await _planRepository.AddAsync(NewPlan);

            PlanResponse responseDto = new PlanResponse()
            {
                PlanId = planRes.Id,
                PlanName = planRes.PlanName,
                ProductId = planRes.InsuranceProductId,
                IsActive = planRes.IsActive
            };
            return responseDto;
        }

        public async Task<PolicyPlan?> UpdateAsync(int id, PolicyPlan plan)
        {
            var existing = await _planRepository.GetByIdAsync(id);

            if (existing == null)
                return null;
            if (plan.CoverageAmount <= plan.PremiumAmount)
            {
                throw new Exception("Coverage amount must be higher than premium amount.");
            }
            existing.PlanName = plan.PlanName;
            existing.CoverageAmount = plan.CoverageAmount;
            existing.PremiumAmount = plan.PremiumAmount;
            existing.PremiumType = plan.PremiumType;
            existing.TermsAndConditions = plan.TermsAndConditions;
            existing.DurationYears = plan.DurationYears;
            existing.UpdatedDate = DateTime.UtcNow;

            return await _planRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var existing = await _planRepository.GetByIdAsync(id);

            if (existing == null)
                return false;

            if (!existing.IsActive)
                return true;

            return await _planRepository.DeactivateAsync(id);
        }
    }
}
