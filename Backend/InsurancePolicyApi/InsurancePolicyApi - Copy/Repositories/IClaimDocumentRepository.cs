using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IClaimDocumentRepository
    {
        Task<ClaimDocument> AddDocumentAsync(ClaimDocument document);

        Task<IEnumerable<ClaimDocument>> GetDocumentsByClaimAsync(int claimId);
    }
}
