using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IClaimService
    {
        Task<ClaimResponse> RaiseClaimAsync(ClaimRequest claim);

        Task<ClaimResponse?> ReviewClaimAsync(int claimId, ClaimReviewRequest crr);

        Task<ClaimResponse?> ApproveClaimAsync(int claimId);

        Task<ClaimResponse?> RejectClaimAsync(int claimId);

        Task<IEnumerable<ClaimResponse>> GetByPolicyAsync(int policyId);

        Task<IEnumerable<ClaimResponse>> GetClaimsAsync(int userId);
        Task<IEnumerable<ClaimResponse>> GetAllClaimsAsync();
        Task<PagedResponse<ClaimResponse>> GetClaimsAsync(PageQuery pagequery);
    }
}
