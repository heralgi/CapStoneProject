using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class ClaimStatusHistoryService : IClaimStatusHistoryService
    {
        private readonly IClaimStatusHistoryRepository _historyRepository;
        private readonly IClaimRepository _claimRepository;
        private readonly IUserRepository _userRepository;

        public ClaimStatusHistoryService(
            IClaimStatusHistoryRepository historyRepository,
            IClaimRepository claimRepository,
            IUserRepository userRepository)
        {
            _historyRepository = historyRepository;
            _claimRepository = claimRepository;
            _userRepository = userRepository;
        }

        public async Task<ClaimStatusHistory> AddStatusHistoryAsync(ClaimStatusHistory history)
        {
            var claim = await _claimRepository.GetByIdAsync(history.ClaimId);

            if (claim == null)
                throw new Exception("Claim not found.");

            var user = await _userRepository.GetByIdAsync(history.UpdatedByUserId);

            if (user == null)
                throw new Exception("User not found.");

            history.UpdatedDate = DateTime.UtcNow;

            return await _historyRepository.AddStatusHistoryAsync(history);
        }

        public async Task<IEnumerable<ClaimStatusHistory>> GetHistoryByClaimAsync(int claimId)
        {
            return await _historyRepository.GetHistoryByClaimAsync(claimId);
        }
    }
}
