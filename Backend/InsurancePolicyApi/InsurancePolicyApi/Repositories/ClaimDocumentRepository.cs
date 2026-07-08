using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class ClaimDocumentRepository : IClaimDocumentRepository
    {
        private readonly AppDbContext _ctx;

        public ClaimDocumentRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ClaimDocument> AddDocumentAsync(ClaimDocument document)
        {
            await _ctx.ClaimDocuments.AddAsync(document);

            await _ctx.SaveChangesAsync();

            return document;
        }

        public async Task<IEnumerable<ClaimDocument>> GetDocumentsByClaimAsync(int claimId)
        {
            return await _ctx.ClaimDocuments
                .Where(d => d.ClaimId == claimId)
                .ToListAsync();
        }
    }
}
