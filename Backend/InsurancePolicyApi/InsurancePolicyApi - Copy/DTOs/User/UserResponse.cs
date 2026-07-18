namespace InsurancePolicyApi.DTOs.User
{
    /// <summary>User details returned to clients (§11.3). Never includes the password (DTO-RUL-005).</summary>
    public class UserResponse
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public string Role { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
