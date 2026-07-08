using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IClaimStatusHistoryService
    {
        Task<ClaimStatusHistory> AddStatusHistoryAsync(ClaimStatusHistory history);

        Task<IEnumerable<ClaimStatusHistory>> GetHistoryByClaimAsync(int claimId);
    }
}
