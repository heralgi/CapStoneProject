using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Policy
{
    /// <summary>
    /// A customer purchasing a policy for themselves (§11.7).
    /// The customer is derived from the authenticated account — never supplied (§31).
    /// </summary>
    public class CustomerPolicyPurchaseRequest
    {
        /// <summary>Reference to the plan being purchased; must be active (§17.5).</summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid plan reference is required.")]
        public int PlanId { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
