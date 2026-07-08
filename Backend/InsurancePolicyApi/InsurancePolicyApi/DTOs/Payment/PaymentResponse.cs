namespace InsurancePolicyApi.DTOs.Payment
{
    /// <summary>Premium payment details returned to clients (§11.8).</summary>
    public class PaymentResponse
    {
        public int PaymentId { get; set; }

        public string PolicyNumber { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMode { get; set; } = null!;

        public string TransactionReference { get; set; } = null!;

        public string PaymentStatus { get; set; } = null!;
    }
}
