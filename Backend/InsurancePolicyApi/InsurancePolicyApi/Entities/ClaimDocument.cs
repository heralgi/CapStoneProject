namespace InsurancePolicyApi.Entities
{
    /// <summary>A document reference submitted for claim verification (§9.8). Must be linked to a claim (DOC-BR-004, MAP-BR-006).</summary>
    public class ClaimDocument
    {
        public int Id { get; set; }

        /// <summary>FK to the associated claim.</summary>
        public int ClaimId { get; set; }

        public string DocumentName { get; set; } = null!;

        public string DocumentType { get; set; } = null!;

        /// <summary>File name, URL, or text reference — actual upload optional (DOC-BR-002/003).</summary>
        public string DocumentReference { get; set; } = null!;

        public DateTime UploadedDate { get; set; }

        // Navigation
        public Claim Claim { get; set; } = null!;
    }
}
