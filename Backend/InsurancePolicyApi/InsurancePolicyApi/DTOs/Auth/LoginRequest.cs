using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Auth
{
    /// <summary>Login credentials (§11.2).</summary>
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
