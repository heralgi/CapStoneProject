using System.ComponentModel.DataAnnotations;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.Product
{
    /// <summary>Admin create/update of an insurance product (§11.5, §17.3).</summary>
    public class ProductRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ProductName { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(ProductType))]
        public ProductType ProductType { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
