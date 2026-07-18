using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>A purchasable plan under an insurance product (§9.4). Must belong to a product (PLN-BR-001, MAP-BR-002).</summary>
    public class PolicyPlan
    {
        public int Id { get; set; }

        /// <summary>FK to the owning insurance product.</summary>
        public int InsuranceProductId { get; set; }

        public string PlanName { get; set; } = null!;

        /// <summary>Maximum coverage offered; must be &gt; 0 and &gt; premium (PLN-BR-002/004).</summary>
        public decimal CoverageAmount { get; set; }

        /// <summary>Required premium; treated as the initial premium for activation (PLN-BR-003/007).</summary>
        public decimal PremiumAmount { get; set; }

        public PremiumType PremiumType { get; set; }

        /// <summary>Policy duration in years; must be &gt; 0.</summary>
        public int DurationYears { get; set; }

        public string TermsAndConditions { get; set; } = null!;

        /// <summary>Only active plans can be purchased (PLN-BR-005).</summary>
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation
        public InsuranceProduct InsuranceProduct { get; set; } = null!;

        // 1:M — a plan can be purchased as multiple separate policies (MAP-003).
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
    }
}
