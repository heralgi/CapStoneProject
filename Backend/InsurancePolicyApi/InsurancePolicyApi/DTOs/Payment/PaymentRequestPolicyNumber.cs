using InsurancePolicyApi.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Payment
{
    public class PaymentRequestPolicyNumber
    {
        [Required]
        public string PolicyNumber { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentMode))]
        public PaymentMode PaymentMode { get; set; }

        /// <summary>Unique transaction reference (PAY-BR-003).</summary>
        [Required]
        [StringLength(100)]
        public string TransactionReference { get; set; } = null!;

        public DateTime PaymentDate { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
