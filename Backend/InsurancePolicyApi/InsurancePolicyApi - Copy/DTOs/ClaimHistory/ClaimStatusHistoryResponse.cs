namespace InsurancePolicyApi.DTOs.ClaimHistory
{
    /// <summary>A single claim status-change record returned to clients (§9.9, HIS-BR-003).</summary>
    public class ClaimStatusHistoryResponse
    {
        public int HistoryId { get; set; }

        public string ClaimNumber { get; set; } = null!;

        public string PreviousStatus { get; set; } = null!;

        public string NewStatus { get; set; } = null!;

        public string? Remarks { get; set; }

        /// <summary>Name of the user who performed the status change.</summary>
        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}
