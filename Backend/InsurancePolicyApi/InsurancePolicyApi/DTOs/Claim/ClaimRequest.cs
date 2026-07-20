using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Claim
{
    /// <summary>
    /// A customer raising a claim against their own active policy (§11.9, §17.7).
    /// Policy-ownership, policy-must-be-Active, incident-not-future and
    /// amount-not-exceeding-coverage are enforced in the service layer.
    /// </summary>
    public class ClaimRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid policy reference is required.")]
        public int PolicyId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Claim amount must be greater than zero.")]
        public decimal ClaimAmount { get; set; }

        [Required]
        [StringLength(1000)]
        public string ClaimReason { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime IncidentDate { get; set; }

        /// <summary>At least one supporting document reference is required (DOC-BR-001).</summary>
        //[Required]
        //[MinLength(1, ErrorMessage = "At least one supporting document is required.")]
        //public List<ClaimDocumentRequest> SupportingDocuments { get; set; } = new();
    }
}
