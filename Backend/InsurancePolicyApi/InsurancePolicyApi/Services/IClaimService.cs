using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IClaimService
    {
        Task<ClaimResponse> RaiseClaimAsync(ClaimRequest claim);

        Task<ClaimResponse?> ReviewClaimAsync(int claimId);

        Task<ClaimResponse?> ApproveClaimAsync(int claimId);

        Task<ClaimResponse?> RejectClaimAsync(int claimId);

        Task<IEnumerable<ClaimResponse>> GetByPolicyAsync(int policyId);

        Task<IEnumerable<ClaimResponse>> GetClaimsAsync(int userId);
        Task<IEnumerable<ClaimResponse>> GetClaimsAsync();
    }
}
