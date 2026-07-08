namespace InsurancePolicyApi.DTOs.Auth
{
    /// <summary>Returned only after successful login (§11.2, DTO-RUL-006).</summary>
    public class LoginResponse
    {
        public string Token { get; set; } = null!;

        public bool isSuccess { get; set; }

        public int? UserId { get; set; }

        public string? UserName { get; set; }

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
