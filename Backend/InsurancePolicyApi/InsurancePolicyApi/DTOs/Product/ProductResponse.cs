namespace InsurancePolicyApi.DTOs.Product
{
    /// <summary>Insurance product details returned to clients (§11.5).</summary>
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
