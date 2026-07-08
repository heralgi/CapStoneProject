using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimStatusHistoriesController : ControllerBase
    {
        private readonly IClaimStatusHistoryService _service;

        public ClaimStatusHistoriesController(IClaimStatusHistoryService service)
        {
            _service = service;
        }

        // GET: api/claimstatushistories/claim/5
        [HttpGet("claim/{claimId:int}")]
        public async Task<IActionResult> GetHistoryByClaim(int claimId)
        {
            var history = await _service.GetHistoryByClaimAsync(claimId);

            return Ok(history);
        }

        // POST: api/claimstatushistories
        [HttpPost]
        public async Task<IActionResult> AddStatusHistory(ClaimStatusHistory history)
        {
            var result = await _service.AddStatusHistoryAsync(history);

            return Ok(result);
        }
    }
}
