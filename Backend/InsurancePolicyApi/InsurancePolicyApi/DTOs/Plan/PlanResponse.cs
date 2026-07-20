using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Plan
{
    /// <summary>Policy plan details returned to clients (§11.6), including parent product info.</summary>
    public class PlanResponse
    {
        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public string PlanName { get; set; } = null!;
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public PremiumType PremiumType { get; set; }
        public int DurationYears { get; set; }
        public string TermsAndConditions { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
