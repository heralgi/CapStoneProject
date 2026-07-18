namespace InsurancePolicyApi.DTOs.Customer
{
    /// <summary>Customer profile details returned to clients (§11.4).</summary>
    public class CustomerResponse
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string PinCode { get; set; } = null!;

        public string NomineeName { get; set; } = null!;

        public string NomineeRelation { get; set; } = null!;
    }
}
