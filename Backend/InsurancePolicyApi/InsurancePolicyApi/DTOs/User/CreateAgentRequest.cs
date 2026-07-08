using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.User
{
    /// <summary>
    /// Admin-only creation of an agent account (§12.2 FR-USER-004, ASM-004).
    /// The Agent role is assigned by the service, not supplied by the client.
    /// </summary>
    public class CreateAgentRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string MobileNumber { get; set; } = null!;
    }
}
