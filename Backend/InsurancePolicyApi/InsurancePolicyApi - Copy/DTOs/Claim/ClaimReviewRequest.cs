using System.ComponentModel.DataAnnotations;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Claim
{
    /// <summary>
    /// An agent reviewing a claim and recommending a decision (§11.9, CLC-RUL-003).
    /// RecommendedStatus must be RecommendedForApproval or RecommendedForRejection — validated in the service.
    /// </summary>
    public class ClaimReviewRequest
    {
        [Required]
        [EnumDataType(typeof(ClaimStatus))]
        public ClaimStatus RecommendedStatus { get; set; }

        [Required]
        [StringLength(1000)]
        public string Remarks { get; set; } = null!;
    }
}
