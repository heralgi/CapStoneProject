using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Policy
{
    /// <summary>An agent or admin issuing a policy on behalf of a customer (§11.7, FR-POL-002).</summary>
    public class AgentPolicyIssueRequest
    {
        /// <summary>Reference to the customer the policy is issued to (required for issuance — §17.5).</summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid customer reference is required.")]
        public int CustomerId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid plan reference is required.")]
        public int PlanId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
