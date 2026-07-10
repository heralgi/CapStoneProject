namespace InsurancePolicyApi.DTOs.Claim
{
    /// <summary>Claim details returned to clients (§11.9).</summary>
    public class ClaimResponse
    {
        public int ClaimId { get; set; }

        public string ClaimNumber { get; set; } = null!;

        public string PolicyNumber { get; set; } = null!;

        public int PolicyId { get; set; }

        public decimal ClaimAmount { get; set; }

        public string ClaimReason { get; set; } = null!;

        public DateTime IncidentDate { get; set; }

        public string ClaimStatus { get; set; } = null!;

        public string? InternalStaffRemarks { get; set; }

        public string? AdminRemarks { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
