using InsurancePolicyApi.DTOs.Common;

namespace InsurancePolicyApi.DTOs.Plan
{
    /// <summary>Filters for the paginated plan listing (§26.5): product, active status.</summary>
    public class PlanQuery : PageQuery
    {
        public int? ProductId { get; set; }

        public bool? IsActive { get; set; }
    }
}
