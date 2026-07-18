using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Payment
{
    /// <summary>Filters for the paginated payment listing (§26.5): policy, payment status.</summary>
    public class PaymentQuery : PageQuery
    {
        public int? PolicyId { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }
    }
}
