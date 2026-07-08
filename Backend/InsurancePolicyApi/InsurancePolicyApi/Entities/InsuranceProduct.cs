using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>The main insurance category (§9.3). Deactivated rather than deleted (PRD-BR-003/004, DRD-002).</summary>
    public class InsuranceProduct
    {
        public int Id { get; set; }

        /// <summary>Unique product name (PRD-BR-001).</summary>
        public string ProductName { get; set; } = null!;

        public ProductType ProductType { get; set; }

        public string Description { get; set; } = null!;

        /// <summary>Only active products are available for new plans/purchases (PRD-BR-002).</summary>
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // 1:M — a product can have multiple plans (MAP-002).
        public ICollection<PolicyPlan> Plans { get; set; } = new List<PolicyPlan>();
    }
}
