using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IPolicyRepository _policyRepository;

        public ClaimService(
            IClaimRepository claimRepository,
            IPolicyRepository policyRepository)
        {
            _claimRepository = claimRepository;
            _policyRepository = policyRepository;
        }

        public async Task<Claim> RaiseClaimAsync(Claim claim)
        {
            var policy = await _policyRepository.GetByIdAsync(claim.PolicyId);

            if (policy == null)
                throw new Exception("Policy not found.");

            if (policy.PolicyStatus != PolicyStatus.Active)
                throw new Exception("Claim can only be raised against an active policy.");

            claim.ClaimNumber = Guid.NewGuid().ToString("N")[..12].ToUpper();
            claim.ClaimStatus = ClaimStatus.Submitted;
            claim.CreatedDate = DateTime.UtcNow;

            return await _claimRepository.RaiseClaimAsync(claim);
        }

        public async Task<Claim?> ReviewClaimAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;

            claim.ClaimStatus = ClaimStatus.UnderReview;

            return await _claimRepository.ReviewClaimAsync(claim);
        }

        public async Task<Claim?> ApproveClaimAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;

            claim.ClaimStatus = ClaimStatus.Approved;

            return await _claimRepository.ApproveClaimAsync(claim);
        }

        public async Task<Claim?> RejectClaimAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;

            claim.ClaimStatus = ClaimStatus.Rejected;

            return await _claimRepository.RejectClaimAsync(claim);
        }

        public async Task<IEnumerable<Claim>> GetByPolicyAsync(int policyId)
        {
            return await _claimRepository.GetByPolicyAsync(policyId);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await _claimRepository.GetClaimsAsync();
        }
    }
}
