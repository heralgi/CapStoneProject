using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IClaimDocumentService
    {
        Task<ClaimDocument> AddDocumentAsync(ClaimDocumentRequest document);

        Task<IEnumerable<ClaimDocument>> GetDocumentsByClaimAsync(int claimId);
    }
}
