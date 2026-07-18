using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>A simulated payment record against a policy (§9.6). Must be linked to a policy (PAY-BR-001, MAP-BR-004).</summary>
    public class PremiumPayment
    {
        public int Id { get; set; }

        /// <summary>FK to the associated policy.</summary>
        public int PolicyId { get; set; }

        /// <summary>Payment amount; must be &gt; 0 (PAY-BR-002).</summary>
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMode PaymentMode { get; set; }

        /// <summary>Unique transaction reference (PAY-BR-003, ID-RUL-003).</summary>
        public string TransactionReference { get; set; } = null!;

        /// <summary>Only Success affects activation/total premium (PMT-RUL-003..005).</summary>
        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation
        public Policy Policy { get; set; } = null!;
    }
}
