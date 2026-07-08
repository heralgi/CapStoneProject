using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService _service;

        public PoliciesController(IPolicyService service)
        {
            _service = service;
        }

        // GET: api/policies
        [HttpGet]
        public async Task<IActionResult> GetPolicies()
        {
            return Ok(await _service.GetPoliciesAsync());
        }

        // GET: api/policies/POL12345
        [HttpGet("{policyNumber}")]
        public async Task<IActionResult> GetByPolicyNumber(string policyNumber)
        {
            var policy = await _service.GetByPolicyNumberAsync(policyNumber);

            if (policy == null)
                return NotFound();

            return Ok(policy);
        }

        // GET: api/policies/customer/5
        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            return Ok(await _service.GetByCustomerIdAsync(customerId));
        }

        // POST: api/policies/purchase
        [HttpPost("purchase/{Id:int}")]
        public async Task<IActionResult> PurchasePolicy(int Id, CustomerPolicyPurchaseRequest request)
        {
            var result = await _service.PurchasePolicyAsync(Id, request);

            return CreatedAtAction(
                nameof(GetByPolicyNumber),
                new { policyNumber = result.PolicyNumber },
                result);
        }

        // PUT: api/policies/issue/5
        [HttpPut("issue/{policyId:int}")]
        public async Task<IActionResult> IssuePolicy(int policyId)
        {
            var result = await _service.IssuePolicyAsync(policyId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
