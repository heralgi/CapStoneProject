using InsurancePolicyApi.DTOs.Product;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public InsuranceProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/insuranceproducts
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();

            return Ok(products);
        }

        // GET: api/insuranceproducts/5
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/insuranceproducts
        [Authorize(Roles=nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Add(ProductRequest request)
        {
            var created = await _service.AddAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/insuranceproducts/5
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, InsuranceProduct product)
        {
            var updated = await _service.UpdateAsync(id, product);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // PUT: api/insuranceproducts/deactivate/5
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("deactivate/{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _service.DeactivateAsync(id);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Insurance product deactivated successfully."
            });
        }
    }
}
