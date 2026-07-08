namespace InsurancePolicyApi.Entities
{
    /// <summary>Customer profile information (§9.2). Must be linked to a customer-role user (CUS-BR-001, MAP-BR-001).</summary>
    public class Customer
    {
        public int Id { get; set; }

        /// <summary>FK to the owning user account (unique — one profile per user, CUS-BR-002).</summary>
        public int UserId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string PinCode { get; set; } = null!;

        public string NomineeName { get; set; } = null!;

        public string NomineeRelation { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation
        public User User { get; set; } = null!;

        // 1:M — a customer can hold multiple policies (MAP-004).
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
    }
}
