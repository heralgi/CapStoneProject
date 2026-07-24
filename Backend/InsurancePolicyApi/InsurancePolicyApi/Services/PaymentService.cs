using InsurancePolicyApi.DTOs.Payment;
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

        public async Task<PaymentResponse> RecordPaymentAsync(PaymentRequest payment)
        {
            var policy = await _policyRepository.GetByIdAsync(payment.PolicyId);

            if (policy == null)
                throw new Exception("Policy not found.");

            if (policy.PolicyStatus != PolicyStatus.PendingPayment)
                throw new Exception("Premium payment can only be made for PendingPayment policies.");

            if (payment.Amount < 1)
                throw new Exception("Amount must be greater than zero");

            if (payment.Amount != policy.PolicyPlan.PremiumAmount)
                throw new Exception("Amount must be equal to Premium");

            payment.PaymentDate = DateTime.UtcNow;
            payment.PaymentStatus = PaymentStatus.Success;

            PremiumPayment premiumpayment = new PremiumPayment()
            {
                PolicyId = payment.PolicyId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMode = payment.PaymentMode,
                TransactionReference = payment.TransactionReference,
                PaymentStatus = payment.PaymentStatus,
                CreatedDate = DateTime.UtcNow
            };

            var responsePayment = await _paymentRepository.RecordPaymentAsync(premiumpayment);
            policy.PolicyStatus = PolicyStatus.Active;
            policy.TotalPremiumPaid = policy.TotalPremiumPaid + payment.Amount;
            await _policyRepository.UpdateAsync(policy);

            PaymentResponse responseDto = new PaymentResponse()
            {
                PaymentId = responsePayment.Id,
                PolicyNumber = responsePayment.Policy.PolicyNumber,
                Amount = responsePayment.Amount,
                PaymentDate = responsePayment.PaymentDate,
                PaymentMode = responsePayment.PaymentMode.ToString(),
                TransactionReference = responsePayment.TransactionReference,
                PaymentStatus = responsePayment.PaymentStatus.ToString()
            };

            return responseDto;
        }

        public async Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId)
        {
            return await _paymentRepository.GetPaymentsByPolicyAsync(policyId);
        }

        public async Task<IEnumerable<PaymentResponse>> GetPaymentHistoryAsync()
        {
            var payments = await _paymentRepository.GetPaymentHistoryAsync();
            List<PaymentResponse> paymentResponses = new List<PaymentResponse>();

            foreach(PremiumPayment payment in payments)
            {
                paymentResponses.Add(new PaymentResponse()
                {
                    PaymentId = payment.Id,
                    PolicyNumber = payment.Policy.PolicyNumber,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentMode = payment.PaymentMode.ToString(),
                    TransactionReference = payment.TransactionReference,
                    PaymentStatus = payment.PaymentStatus.ToString()
                });
            }
            return paymentResponses;
        }
    }
}
