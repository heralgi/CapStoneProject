using InsurancePolicyApi.DTOs.Plan;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyPlansController : ControllerBase
    {
        private readonly IPlanService _service;

        public PolicyPlansController(IPlanService service)
        {
            _service = service;
        }



        // GET: api/policyplans/product/1
        [Authorize]
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var plans = await _service.GetByProductIdAsync(productId);

            return Ok(plans);
        }

        // POST: api/policyplans
        [Authorize(Roles=nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Add(PlanRequest planRequest)
        {
            var created = await _service.AddAsync(planRequest);

            return CreatedAtAction(
                nameof(GetByProductId),
                new { productId = created.ProductId },
                created);
        }

        // PUT: api/policyplans/5
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PlanRequest plan)
        {
            var updated = await _service.UpdateAsync(id, plan);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // PUT: api/policyplans/deactivate/5
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("deactivate/{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _service.DeactivateAsync(id);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Policy plan deactivated successfully."
            });
        }
    }
}
