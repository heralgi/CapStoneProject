using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> GetPolicies()
        {
            int userId = int.Parse(User.FindFirst("userid")!.Value);
            return Ok(await _service.GetPoliciesAsync(userId));
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
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchasePolicy(CustomerPolicyPurchaseRequest request)
        {
            int userId = int.Parse(User.FindFirst("userid")!.Value);
            var result = await _service.PurchasePolicyAsync(userId, request);

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
