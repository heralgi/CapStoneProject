using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class ClaimDocumentService : IClaimDocumentService
    {
        private readonly IClaimDocumentRepository _documentRepository;
        private readonly IClaimRepository _claimRepository;

        public ClaimDocumentService(
            IClaimDocumentRepository documentRepository,
            IClaimRepository claimRepository)
        {
            _documentRepository = documentRepository;
            _claimRepository = claimRepository;
        }

        public async Task<ClaimDocument> AddDocumentAsync(ClaimDocumentRequest document)
        {
            var claim = await _claimRepository.GetByIdAsync(document.ClaimId);

            if (claim == null)
                throw new Exception("Claim not found.");

            ClaimDocument documentpost = new ClaimDocument()
            {

            };

            return await _documentRepository.AddDocumentAsync(document);
        }

        public async Task<IEnumerable<ClaimDocument>> GetDocumentsByClaimAsync(int claimId)
        {
            return await _documentRepository.GetDocumentsByClaimAsync(claimId);
        }
    }
}
