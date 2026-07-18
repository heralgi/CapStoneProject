using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Claim
{
    /// <summary>
    /// A supporting document reference submitted with a claim (§9.8, §11.9).
    /// Actual file upload is optional — a name/URL/text reference is sufficient (DOC-BR-002/003).
    /// </summary>
    public class ClaimDocumentRequest
    {
        [Required]
        public int ClaimId { get; set; }
        [Required]
        [StringLength(200)]
        public string DocumentName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; } = null!;

        /// <summary>File name, URL, or text reference.</summary>
        [Required]
        [StringLength(500)]
        public string DocumentReference { get; set; } = null!;
    }
}
