using InsurancePolicyApi.DTOs.Payment;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremiumPaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PremiumPaymentsController(IPaymentService service)
        {
            _service = service;
        }

        // GET: api/premiumpayments
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPaymentHistory()
        {
            return Ok(await _service.GetPaymentHistoryAsync());
        }

        // GET: api/premiumpayments/policy/5
        [HttpGet("policy/{policyId:int}")]
        public async Task<IActionResult> GetPaymentsByPolicy(int policyId)
        {
            return Ok(await _service.GetPaymentsByPolicyAsync(policyId));
        }

        // POST: api/premiumpayments
        [HttpPost]
        public async Task<IActionResult> RecordPayment(PaymentRequest payment)
        {
            var result = await _service.RecordPaymentAsync(payment);

            return Ok(result);
        }
    }
}
