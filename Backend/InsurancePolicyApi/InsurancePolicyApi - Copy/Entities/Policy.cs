using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>An insurance policy purchased by or issued to a customer (§9.5).</summary>
    public class Policy
    {
        public int Id { get; set; }

        /// <summary>Unique business-facing policy number (POL-BR-003, ID-RUL-001).</summary>
        public string PolicyNumber { get; set; } = null!;

        /// <summary>FK — every policy belongs to one customer (POL-BR-001, MAP-BR-003).</summary>
        public int CustomerId { get; set; }

        /// <summary>FK — every policy is linked to one policy plan (POL-BR-002).</summary>
        public int PolicyPlanId { get; set; }

        public DateTime StartDate { get; set; }

        /// <summary>Derived from start date + plan duration.</summary>
        public DateTime EndDate { get; set; }

        /// <summary>Begins as PendingPayment (POL-BR-004).</summary>
        public PolicyStatus PolicyStatus { get; set; } = PolicyStatus.PendingPayment;

        /// <summary>Total of successful premium payments (PAY-BR-004).</summary>
        public decimal TotalPremiumPaid { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation
        public Customer Customer { get; set; } = null!;
        public PolicyPlan PolicyPlan { get; set; } = null!;

        // 1:M — a policy can have multiple payments (MAP-005) and claims (MAP-006).
        public ICollection<PremiumPayment> Payments { get; set; } = new List<PremiumPayment>();
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
