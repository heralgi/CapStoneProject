
using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IClaimRepository
    {
        Task<Claim> RaiseClaimAsync(Claim claim);

        Task<Claim?> ReviewClaimAsync(Claim claim);

        Task<Claim?> ApproveClaimAsync(Claim claim);

        Task<Claim?> RejectClaimAsync(Claim claim);

        Task<IEnumerable<Claim>> GetByPolicyAsync(int policyId);

        Task<PagedResponse<ClaimResponse>> GetClaimsAsync(PageQuery pagequery);
        Task<IEnumerable<ClaimResponse>> GetAllClaimsAsync();

        Task<Claim?> GetByIdAsync(int id);
    }
}
