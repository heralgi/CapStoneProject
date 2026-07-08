using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class PremiumPaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPolicyRepository _policyRepository;

        public PremiumPaymentService(
            IPaymentRepository paymentRepository,
            IPolicyRepository policyRepository)
        {
            _paymentRepository = paymentRepository;
            _policyRepository = policyRepository;
        }

        public async Task<PremiumPayment> RecordPaymentAsync(PremiumPayment payment)
        {
            var policy = await _policyRepository.GetByIdAsync(payment.PolicyId);

            if (policy == null)
                throw new Exception("Policy not found.");

            if (policy.PolicyStatus != PolicyStatus.Active)
                throw new Exception("Premium payment can only be made for active policies.");

            payment.PaymentDate = DateTime.UtcNow;
            payment.PaymentStatus = PaymentStatus.Success;

            return await _paymentRepository.RecordPaymentAsync(payment);
        }

        public async Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId)
        {
            return await _paymentRepository.GetPaymentsByPolicyAsync(policyId);
        }

        public async Task<IEnumerable<PremiumPayment>> GetPaymentHistoryAsync()
        {
            return await _paymentRepository.GetPaymentHistoryAsync();
        }
    }
}
