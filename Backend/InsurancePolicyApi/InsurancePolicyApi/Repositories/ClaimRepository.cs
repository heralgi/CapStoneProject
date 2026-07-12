using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext _ctx;

        public ClaimRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Claim> RaiseClaimAsync(Claim claim)
        {
            await _ctx.Claims.AddAsync(claim);

            await _ctx.SaveChangesAsync();

            return claim;
        }

        public async Task<Claim?> ReviewClaimAsync(Claim claim)
        {
            _ctx.Claims.Update(claim);

            await _ctx.SaveChangesAsync();

            return await _ctx.Claims.Include(p => p.Policy).Where(c => c.Id == claim.Id).FirstOrDefaultAsync();
        }

        public async Task<Claim?> ApproveClaimAsync(Claim claim)
        {
            _ctx.Claims.Update(claim);

            await _ctx.SaveChangesAsync();

            return claim;
        }

        public async Task<Claim?> RejectClaimAsync(Claim claim)
        {
            _ctx.Claims.Update(claim);

            await _ctx.SaveChangesAsync();

            return claim;
        }

        public async Task<IEnumerable<Claim>> GetByPolicyAsync(int policyId)
        {
            return await _ctx.Claims
                .Include(p => p.Policy)
                .Where(c => c.PolicyId == policyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await _ctx.Claims
                .Include(p => p.Policy)
                .Include(c => c.Policy)
                .ToListAsync();
        }

        public async Task<Claim?> GetByIdAsync(int id)
        {
            return await _ctx.Claims.FindAsync(id);
        }
    }
}
