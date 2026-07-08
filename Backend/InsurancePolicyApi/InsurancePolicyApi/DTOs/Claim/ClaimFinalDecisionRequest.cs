using System.ComponentModel.DataAnnotations;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Claim
{
    /// <summary>
    /// An admin making the final claim decision (§11.9, CLC-RUL-004).
    /// FinalDecisionStatus must be Approved or Rejected — validated in the service.
    /// </summary>
    public class ClaimFinalDecisionRequest
    {
        [Required]
        [EnumDataType(typeof(ClaimStatus))]
        public ClaimStatus FinalDecisionStatus { get; set; }

        [Required]
        [StringLength(1000)]
        public string Remarks { get; set; } = null!;
    }
}
