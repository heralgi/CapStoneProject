using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IClaimDocumentService
    {
        Task<ClaimDocument> AddDocumentAsync(ClaimDocument document);

        Task<IEnumerable<ClaimDocument>> GetDocumentsByClaimAsync(int claimId);
    }
}
