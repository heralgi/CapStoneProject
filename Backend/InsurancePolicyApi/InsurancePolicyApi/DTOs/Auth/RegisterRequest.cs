using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Password must be between 5 and 100 characters.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = null!;

        [Required]
        [Phone(ErrorMessage = "Please enter a valid mobile number.")]
        [StringLength(15)]
        public string MobileNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{6}$",
            ErrorMessage = "PIN Code must be exactly 6 digits.")]
        public string PinCode { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string NomineeName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NomineeRelation { get; set; } = null!;
    }
}