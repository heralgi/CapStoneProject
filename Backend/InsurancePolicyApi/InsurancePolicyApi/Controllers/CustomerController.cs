using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    using InsurancePolicyApi.DTOs.Common;
    using InsurancePolicyApi.Entities;
    using InsurancePolicyApi.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageQuery pq)
        {
            var customers = await _customerService.GetAllAsync(pq);

            return Ok(customers);
        }

        // GET: api/customers/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET: api/customers/user/10
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var customer = await _customerService.GetByUserIdAsync(userId);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> CreateProfile(Customer customer)
        {
            var created = await _customerService.CreateProfileAsync(customer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/customers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProfile(int id, Customer customer)
        {
            var updated = await _customerService.UpdateProfileAsync(id, customer);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
    }
}
