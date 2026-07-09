using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly AppDbContext _ctx;

        public PolicyRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Policy> PurchasePolicyAsync(Policy policy)
        {
            await _ctx.Policies.AddAsync(policy);

            await _ctx.SaveChangesAsync();

            return policy;
        }

        public async Task<Policy?> IssuePolicyAsync(Policy policy)
        {
            _ctx.Policies.Update(policy);

            await _ctx.SaveChangesAsync();

            return policy;
        }

        public async Task<Policy?> GetByPolicyNumberAsync(string policyNumber)
        {
            return await _ctx.Policies
                .Include(p => p.Customer)
                .Include(p => p.PolicyPlan)
                .FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);
        }

        public async Task<IEnumerable<Policy>> GetByCustomerIdAsync(int customerId)
        {
            return await _ctx.Policies
                .Include(p => p.PolicyPlan)
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Policy>> GetPoliciesAsync(int userId)
        {
            var policy = await _ctx.Policies
                .Include(p => p.Customer)
                .Include(p => p.PolicyPlan)
                .Where(p => p.Customer.UserId == userId)
                .ToListAsync();
            return policy;
        }

        public async Task<Policy?> GetByIdAsync(int id)
        {
            return await _ctx.Policies.FindAsync(id);
        }

        public async Task<Policy> UpdateAsync(Policy policy)
        {
            _ctx.Policies.Update(policy);

            await _ctx.SaveChangesAsync();

            return policy;
        }
    }
}
