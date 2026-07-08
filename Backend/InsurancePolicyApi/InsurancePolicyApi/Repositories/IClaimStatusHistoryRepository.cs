using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IClaimStatusHistoryRepository
    {
        Task<ClaimStatusHistory> AddStatusHistoryAsync(ClaimStatusHistory history);

        Task<IEnumerable<ClaimStatusHistory>> GetHistoryByClaimAsync(int claimId);
    }
}
