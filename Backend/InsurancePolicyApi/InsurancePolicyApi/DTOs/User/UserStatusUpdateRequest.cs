using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.User
{
    /// <summary>Admin activates or deactivates a user account (§11.3, FR-USER-002/003).</summary>
    public class UserStatusUpdateRequest
    {
        [Required]
        public bool IsActive { get; set; }

        /// <summary>Reason or remarks for the status change (for audit/logging).</summary>
        [StringLength(500)]
        public string? Reason { get; set; }
    }
}
