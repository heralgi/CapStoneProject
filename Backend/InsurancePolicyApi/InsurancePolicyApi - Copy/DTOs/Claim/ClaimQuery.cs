using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Claim
{
    /// <summary>Filters for the paginated claim listing (§26.5): claim status, customer.</summary>
    public class ClaimQuery : PageQuery
    {
        public ClaimStatus? ClaimStatus { get; set; }

        public int? CustomerId { get; set; }
    }
}
