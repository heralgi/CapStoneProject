using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Repositories;
using System.Security.Claims;

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

        public async Task<ClaimResponse> RaiseClaimAsync(ClaimRequest claim)
        {
            var policy = await _policyRepository.GetByIdAsync(claim.PolicyId);

            if (policy == null)
                throw new Exception("Policy not found.");

            if (policy.PolicyStatus != PolicyStatus.Active)
                throw new Exception("Claim can only be raised against an active policy.");

            Entities.Claim claimMod = new Entities.Claim()
            {
                PolicyId = claim.PolicyId,
                ClaimNumber = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                ClaimAmount = claim.ClaimAmount,
                ClaimReason = claim.ClaimReason,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IncidentDate = claim.IncidentDate
            };

            var claimModRes = await _claimRepository.RaiseClaimAsync(claimMod);

            var claimRes = GetClaimResponse(claimModRes);

            return claimRes;
        }

        public async Task<ClaimResponse?> ReviewClaimAsync(int claimId, ClaimReviewRequest crr)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;
            if (claim.ClaimStatus == ClaimStatus.Approved || claim.ClaimStatus == ClaimStatus.Rejected)
                throw new Exception("Policy already claimed");

            claim.ClaimStatus = crr.RecommendedStatus;
            claim.AdminRemarks = crr.Remarks;

            var claimModRes = await _claimRepository.ReviewClaimAsync(claim);
            var claimRes = GetClaimResponse(claimModRes);

            return claimRes;
        }

        public async Task<ClaimResponse?> ApproveClaimAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;

            claim.ClaimStatus = ClaimStatus.Approved;

            var claimModRes = await _claimRepository.ApproveClaimAsync(claim);
            var claimRes = GetClaimResponse(claimModRes);

            return claimRes;
        }

        public async Task<ClaimResponse?> RejectClaimAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;

            claim.ClaimStatus = ClaimStatus.Rejected;

            var claimModRes = await _claimRepository.RejectClaimAsync(claim);
            var claimRes = GetClaimResponse(claimModRes);

            return claimRes;
        }

        public async Task<IEnumerable<ClaimResponse>> GetByPolicyAsync(int policyId)
        {
            var claimList = await _claimRepository.GetByPolicyAsync(policyId);
            var claims = new List<ClaimResponse>();
            foreach (Entities.Claim claim in claimList)
            {
                claims.Add(GetClaimResponse(claim));
            }
            return claims;
        }

        public async Task<IEnumerable<ClaimResponse>> GetClaimsAsync(int userId)
        {
            var policies = await _policyRepository.GetPoliciesAsync(userId);
            var claims = new List<ClaimResponse>();

            foreach (Policy policy in policies)
            {
                var claimList = await _claimRepository.GetByPolicyAsync(policy.Id);

                foreach (Entities.Claim claim in claimList)
                {
                    claims.Add(GetClaimResponse(claim));
                }
            }

            return claims;
        }

        public async Task<IEnumerable<ClaimResponse>> GetAllClaimsAsync()
        {
            var claims = await _claimRepository.GetAllClaimsAsync();
            return claims;
        }


        public async Task<PagedResponse<ClaimResponse>> GetClaimsAsync(PageQuery pagequery)
        {
            var claimList = await _claimRepository.GetClaimsAsync(pagequery);

            /*var claims = new List<ClaimResponse>();
            foreach (Entities.Claim claim in claimList)
            {
                claims.Add(GetClaimResponse(claim));
            }
            return claims;*/
            return claimList;
        }

        ClaimResponse GetClaimResponse(Entities.Claim claim)
        {
            ClaimResponse claimresponse = new ClaimResponse()
            {
                ClaimId = claim.Id,
                PolicyId = claim.PolicyId,
                ClaimNumber = claim.ClaimNumber,
                ClaimAmount = claim.ClaimAmount,
                ClaimReason = claim.ClaimReason,
                ClaimStatus = claim.ClaimStatus.ToString(),
                PolicyNumber = claim.Policy.PolicyNumber,
                IncidentDate = claim.IncidentDate,
                AdminRemarks = claim.AdminRemarks
            };

            return claimresponse;
        }
    }
}
