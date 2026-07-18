using InsurancePolicyApi.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.User
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Password must be between 5 and 100 characters.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase " +
            "letter, one number, and one special character.")]
        public string Password { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string MobileNumber { get; set; } = null!;

        public UserRole Role { get; set; }
    }
}
