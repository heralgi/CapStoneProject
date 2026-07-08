using System.ComponentModel.DataAnnotations;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Plan
{
    /// <summary>
    /// Admin create/update of a policy plan (§11.6, §17.4).
    /// Coverage-greater-than-premium and product-must-be-active are enforced in the service layer.
    /// </summary>
    public class PlanRequest
    {
        /// <summary>Reference to the owning insurance product; must be active (PLN-BR-001).</summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid product reference is required.")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string PlanName { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Coverage amount must be greater than zero.")]
        public decimal CoverageAmount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Premium amount must be greater than zero.")]
        public decimal PremiumAmount { get; set; }

        [Required]
        [EnumDataType(typeof(PremiumType))]
        public PremiumType PremiumType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than zero years.")]
        public int DurationYears { get; set; }

        [Required]
        [StringLength(2000)]
        public string TermsAndConditions { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
