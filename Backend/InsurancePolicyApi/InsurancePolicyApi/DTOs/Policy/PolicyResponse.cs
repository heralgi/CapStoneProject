namespace InsurancePolicyApi.DTOs.Policy
{
    /// <summary>Policy details returned to clients (§11.7).</summary>
    public class PolicyResponse
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = null!;

        public int CustomerId { get; set; }

        public string PlanName { get; set; } = null!;

        public decimal CoverageAmount { get; set; }

        public decimal PremiumAmount { get; set; }

        public string PremiumType { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PolicyStatus { get; set; } = null!;

        public decimal TotalPremiumPaid { get; set; }
    }
}
