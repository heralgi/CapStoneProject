using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Customer
{
    /// <summary>
    /// Create/update a customer profile (§11.4). The owning customer is derived from the
    /// authenticated account, never supplied by the client (§31 ambiguity checklist).
    /// Date-of-birth-must-be-past is enforced in the service layer (§17.2).
    /// </summary>
    public class CustomerRequest
    {
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
        [StringLength(10, MinimumLength = 4)]
        public string PinCode { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string NomineeName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NomineeRelation { get; set; } = null!;
    }
}
