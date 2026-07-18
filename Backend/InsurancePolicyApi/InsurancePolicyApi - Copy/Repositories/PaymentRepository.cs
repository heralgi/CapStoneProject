using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _ctx;

        public PaymentRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PremiumPayment> RecordPaymentAsync(PremiumPayment payment)
        {
            await _ctx.PremiumPayments.AddAsync(payment);

            await _ctx.SaveChangesAsync();

            return payment;
        }

        public async Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId)
        {
            return await _ctx.PremiumPayments
                .Where(p => p.PolicyId == policyId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PremiumPayment>> GetPaymentHistoryAsync()
        {
            return await _ctx.PremiumPayments
                .Include(p => p.Policy)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<PremiumPayment?> GetByIdAsync(int id)
        {
            return await _ctx.PremiumPayments.FindAsync(id);
        }
    }
}
