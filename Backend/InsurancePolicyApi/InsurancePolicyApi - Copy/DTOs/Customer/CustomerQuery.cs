using InsurancePolicyApi.DTOs.Common;

namespace InsurancePolicyApi.DTOs.Customer
{
    /// <summary>Paginated/sorted customer listing for admin and agent (FR-CUS-006). Optional city filter.</summary>
    public class CustomerQuery : PageQuery
    {
        public string? City { get; set; }
    }
}
