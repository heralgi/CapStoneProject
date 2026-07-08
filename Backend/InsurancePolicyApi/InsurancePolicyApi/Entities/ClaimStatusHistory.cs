using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>
    /// Append-only record of a claim's status movement (§9.9).
    /// Every status change is recorded and never modified (HIS-BR-001/002, CLC-RUL-007).
    /// </summary>
    public class ClaimStatusHistory
    {
        public int Id { get; set; }

        /// <summary>FK to the associated claim (MAP-BR-007).</summary>
        public int ClaimId { get; set; }

        public ClaimStatus PreviousStatus { get; set; }

        public ClaimStatus NewStatus { get; set; }

        public string? Remarks { get; set; }

        /// <summary>FK to the user who performed the status change (MAP-009, HIS-BR-003).</summary>
        public int UpdatedByUserId { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation
        public Claim Claim { get; set; } = null!;
        public User UpdatedByUser { get; set; } = null!;
    }
}
