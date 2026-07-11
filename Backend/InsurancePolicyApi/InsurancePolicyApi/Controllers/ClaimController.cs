using InsurancePolicyApi.DTOs.Claim;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _service;

        public ClaimsController(IClaimService service)
        {
            _service = service;
        }

        // GET: api/claims
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetClaims()
        {
            int userId = int.Parse(User.FindFirst("userid")!.Value);
            if (User.FindFirst(System.Security.Claims.ClaimTypes.Role)!.Value == UserRole.Admin.ToString())
            {
                return Ok(await _service.GetClaimsAsync());
            }
            
            return Ok(await _service.GetClaimsAsync(userId));
        }

        // GET: api/claims/policy/5
        [HttpGet("policy/{policyId:int}")]
        public async Task<IActionResult> GetByPolicy(int policyId)
        {
            return Ok(await _service.GetByPolicyAsync(policyId));
        }

        // POST: api/claims/raise
        [HttpPost("raise")]
        public async Task<IActionResult> RaiseClaim(ClaimRequest claim)
        {
            var result = await _service.RaiseClaimAsync(claim);

            return Ok(result);
        }

        // PUT: api/claims/review/5
        //[Authorize(Roles = UserRole.Admin)]
        [HttpPut("review/{claimId:int}")]
        public async Task<IActionResult> ReviewClaim(int claimId, ClaimReviewRequest crr)
        {
            var result = await _service.ReviewClaimAsync(claimId, crr);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PUT: api/claims/approve/5
        [HttpPut("approve/{claimId:int}")]
        public async Task<IActionResult> ApproveClaim(int claimId)
        {
            var result = await _service.ApproveClaimAsync(claimId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PUT: api/claims/reject/5
        [HttpPut("reject/{claimId:int}")]
        public async Task<IActionResult> RejectClaim(int claimId)
        {
            var result = await _service.RejectClaimAsync(claimId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
