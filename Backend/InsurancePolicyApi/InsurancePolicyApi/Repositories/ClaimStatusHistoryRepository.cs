using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class ClaimStatusHistoryRepository : IClaimStatusHistoryRepository
    {
        private readonly AppDbContext _ctx;

        public ClaimStatusHistoryRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ClaimStatusHistory> AddStatusHistoryAsync(ClaimStatusHistory history)
        {
            await _ctx.ClaimStatusHistories.AddAsync(history);

            await _ctx.SaveChangesAsync();

            return history;
        }

        public async Task<IEnumerable<ClaimStatusHistory>> GetHistoryByClaimAsync(int claimId)
        {
            return await _ctx.ClaimStatusHistories
                .Include(h => h.UpdatedByUser)
                .Where(h => h.ClaimId == claimId)
                .OrderBy(h => h.UpdatedDate)
                .ToListAsync();
        }
    }
}
