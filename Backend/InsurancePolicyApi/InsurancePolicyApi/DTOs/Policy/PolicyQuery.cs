using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Policy
{
    /// <summary>Filters for the paginated policy listing (§26.5): policy status, customer.</summary>
    public class PolicyQuery : PageQuery
    {
        public PolicyStatus? PolicyStatus { get; set; }

        public int? CustomerId { get; set; }
    }
}
