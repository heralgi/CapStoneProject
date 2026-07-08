using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IPaymentRepository
    {
        Task<PremiumPayment> RecordPaymentAsync(PremiumPayment payment);

        Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId);

        Task<IEnumerable<PremiumPayment>> GetPaymentHistoryAsync();

        Task<PremiumPayment?> GetByIdAsync(int id);
    }
}
