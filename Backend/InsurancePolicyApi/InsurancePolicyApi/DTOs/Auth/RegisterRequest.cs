namespace InsurancePolicyApi.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

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