using InsurancePolicyApi.DTOs.Payment;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> RecordPaymentAsync(PaymentRequest payment);

        Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId);

        Task<IEnumerable<PaymentResponse>> GetPaymentHistoryAsync();
    }
}
