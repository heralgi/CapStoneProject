using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>An insurance claim submitted against an active policy (§9.7). Must be linked to a policy (CLM-BR-001, MAP-BR-005).</summary>
    public class Claim
    {
        public int Id { get; set; }

        /// <summary>Unique business-facing claim number (CLM-BR-002, ID-RUL-002).</summary>
        public string ClaimNumber { get; set; } = null!;

        /// <summary>FK to the associated policy.</summary>
        public int PolicyId { get; set; }

        /// <summary>Requested amount; &gt; 0 and not exceeding policy coverage (CLM-BR-003/004).</summary>
        public decimal ClaimAmount { get; set; }

        public string ClaimReason { get; set; } = null!;

        /// <summary>Date of incident; must not be in the future (CLM-BR-005).</summary>
        public DateTime IncidentDate { get; set; }

        /// <summary>Starts at Submitted (CLC-RUL-001).</summary>
        public ClaimStatus ClaimStatus { get; set; } = ClaimStatus.Submitted;

        /// <summary>Remarks added by the reviewing agent.</summary>
        public string? InternalStaffRemarks { get; set; }

        /// <summary>Final remarks added by the admin.</summary>
        public string? AdminRemarks { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation
        public Policy Policy { get; set; } = null!;

        // 1:M — a claim can have multiple documents (MAP-007) and status history records (MAP-008).
        public ICollection<ClaimDocument> Documents { get; set; } = new List<ClaimDocument>();
        public ICollection<ClaimStatusHistory> StatusHistory { get; set; } = new List<ClaimStatusHistory>();
    }
}
