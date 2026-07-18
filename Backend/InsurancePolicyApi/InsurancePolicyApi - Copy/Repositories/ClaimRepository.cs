using InsurancePolicyApi.Data;
using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.DTOs.Common;
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

        public async Task<PagedResponse<ClaimResponse>> GetClaimsAsync(PageQuery pagequery)
        {
            var query = _ctx.Claims.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((pagequery.PageNumber - 1) * pagequery.PageSize)
                .Take(pagequery.PageSize)
                .ToListAsync();

            List<ClaimResponse> claimResponseItems = GetClaimResponses(items);

            return new PagedResponse<ClaimResponse>
            {
                Records = claimResponseItems,
                CurrentPage = pagequery.PageNumber,
                PageSize = pagequery.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagequery.PageSize)
            };
        }

        public async Task<Claim?> GetByIdAsync(int id)
        {
            return await _ctx.Claims.FindAsync(id);
        }

        List<ClaimResponse> GetClaimResponses(List<Claim> claims)
        {
            List<ClaimResponse> claimresponses = new List<ClaimResponse>();
            foreach (Claim claim in claims)
            {
                claimresponses.Add(new ClaimResponse()
                {
                    ClaimId = claim.Id,
                    PolicyId = claim.PolicyId,
                    ClaimNumber = claim.ClaimNumber,
                    ClaimAmount = claim.ClaimAmount,
                    ClaimReason = claim.ClaimReason,
                    ClaimStatus = claim.ClaimStatus.ToString(),
                    IncidentDate = claim.IncidentDate,
                    AdminRemarks = claim.AdminRemarks
                });
            }

            return claimresponses;
        }
    }
}
