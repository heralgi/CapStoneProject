using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Product
{
    /// <summary>Filters for the paginated product listing (§26.5): product type, active status.</summary>
    public class ProductQuery : PageQuery
    {
        public ProductType? ProductType { get; set; }

        public bool? IsActive { get; set; }
    }
}
