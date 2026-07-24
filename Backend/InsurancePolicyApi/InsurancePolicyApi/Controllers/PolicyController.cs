using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.Policy;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
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
        public async Task<IActionResult> GetPolicies([FromQuery] PageQuery pq)
        {
            int userId = int.Parse(User.FindFirst("userid")!.Value);
            return Ok(await _service.GetPoliciesAsync(userId, pq));
        }

        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetPolicies()
        {
            int userId = int.Parse(User.FindFirst("userid")!.Value);
            return Ok(await _service.GetPoliciesAsync(userId));
        }

        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.InternalStaff)}")]
        public async Task<IActionResult> GetAllPolicies()
        {
            return Ok(await _service.GetAllPoliciesAsync());
        }

        // GET: api/policies/POL12345
        [HttpGet("{policyNumber}")]
        [Authorize]
        public async Task<IActionResult> GetByPolicyNumber(string policyNumber)
        {
            var policy = await _service.GetByPolicyNumberAsync(policyNumber);

            if (policy == null)
                return NotFound();

            return Ok(policy);
        }

        // GET: api/policies/customer/5
        [HttpGet("customer/{customerId:int}")]
        [Authorize]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            return Ok(await _service.GetByCustomerIdAsync(customerId));
        }

        // POST: api/policies/purchase
        [HttpPost("purchase")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> IssuePolicy(int policyId)
        {
            var result = await _service.IssuePolicyAsync(policyId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
