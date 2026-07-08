
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

        Task<IEnumerable<Claim>> GetClaimsAsync();

        Task<Claim?> GetByIdAsync(int id);
    }
}
