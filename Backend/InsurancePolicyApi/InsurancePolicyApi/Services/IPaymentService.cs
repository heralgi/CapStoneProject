using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IPaymentService
    {
        Task<PremiumPayment> RecordPaymentAsync(PremiumPayment payment);

        Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId);

        Task<IEnumerable<PremiumPayment>> GetPaymentHistoryAsync();
    }
}
