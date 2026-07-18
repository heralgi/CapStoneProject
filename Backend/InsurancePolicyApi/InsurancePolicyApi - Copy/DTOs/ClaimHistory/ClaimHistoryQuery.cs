using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.ClaimHistory
{
    /// <summary>Filters for the paginated claim status history listing (§26.5): claim, updated by, status.</summary>
    public class ClaimHistoryQuery : PageQuery
    {
        public int? ClaimId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public ClaimStatus? Status { get; set; }
    }
}
