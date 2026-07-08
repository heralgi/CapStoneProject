using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.Entities
{
    /// <summary>Account and login information (§9.1).</summary>
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        /// <summary>Unique login email (USR-BR-001).</summary>
        public string Email { get; set; } = null!;

        /// <summary>Securely hashed password — never exposed (USR-BR-002, DTO-RUL-005).</summary>
        public string PasswordHash { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        /// <summary>Exactly one role per user (USR-BR-006).</summary>
        public UserRole Role { get; set; }

        /// <summary>Inactive users cannot log in (USR-BR-005).</summary>
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation — 1:1 with Customer (MAP-001), only for customer-role users.
        public Customer? Customer { get; set; }

        // Navigation — 1:M claim status updates performed by this user (MAP-009).
        public ICollection<ClaimStatusHistory> ClaimStatusUpdates { get; set; } = new List<ClaimStatusHistory>();
    }
}
