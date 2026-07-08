using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _ctx;

        public PlanRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<PolicyPlan>> GetByProductIdAsync(int productId)
        {
            return await _ctx.PolicyPlans
                .Include(p => p.InsuranceProduct)
                .Where(p => p.InsuranceProductId == productId)
                .ToListAsync();
        }

        public async Task<PolicyPlan?> GetByIdAsync(int id)
        {
            return await _ctx.PolicyPlans
                .Include(p => p.InsuranceProduct)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PolicyPlan> AddAsync(PolicyPlan plan)
        {
            await _ctx.PolicyPlans.AddAsync(plan);

            await _ctx.SaveChangesAsync();

            return plan;
        }

        public async Task<PolicyPlan> UpdateAsync(PolicyPlan plan)
        {
            _ctx.PolicyPlans.Update(plan);

            await _ctx.SaveChangesAsync();

            return plan;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var plan = await _ctx.PolicyPlans.FindAsync(id);

            if (plan == null)
                return false;

            plan.IsActive = false;

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
