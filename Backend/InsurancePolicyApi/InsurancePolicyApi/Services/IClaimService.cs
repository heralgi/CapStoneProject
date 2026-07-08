using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IClaimService
    {
        Task<Claim> RaiseClaimAsync(Claim claim);

        Task<Claim?> ReviewClaimAsync(int claimId);

        Task<Claim?> ApproveClaimAsync(int claimId);

        Task<Claim?> RejectClaimAsync(int claimId);

        Task<IEnumerable<Claim>> GetByPolicyAsync(int policyId);

        Task<IEnumerable<Claim>> GetClaimsAsync();
    }
}
