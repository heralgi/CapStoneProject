using InsurancePolicyApi.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.User
{
    public class UpdateUserRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string MobileNumber { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
