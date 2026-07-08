using System.ComponentModel.DataAnnotations;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Payment
{
    /// <summary>Record a simulated premium payment against a policy (§11.8, §17.6).</summary>
    public class PaymentRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid policy reference is required.")]
        public int PolicyId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentMode))]
        public PaymentMode PaymentMode { get; set; }

        /// <summary>Unique transaction reference (PAY-BR-003).</summary>
        [Required]
        [StringLength(100)]
        public string TransactionReference { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
