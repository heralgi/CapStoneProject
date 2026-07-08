using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

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

        public async Task<IEnumerable<PolicyPlan>> GetByProductIdAsync(int productId)
        {
            return await _planRepository.GetByProductIdAsync(productId);
        }

        public async Task<PolicyPlan> AddAsync(PolicyPlan plan)
        {
            var product = await _productRepository.GetByIdAsync(plan.InsuranceProductId);

            if (product == null)
                throw new Exception("Insurance Product does not exist.");

            if (!product.IsActive)
                throw new Exception("Cannot add a plan to an inactive insurance product.");
            if (plan.CoverageAmount <= plan.PremiumAmount)
            {
                throw new Exception("Coverage amount must be higher than premium amount.");
            }
            plan.IsActive = true;

            return await _planRepository.AddAsync(plan);
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
