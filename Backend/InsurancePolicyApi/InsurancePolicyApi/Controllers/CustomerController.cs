using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    using InsurancePolicyApi.DTOs.Common;
    using InsurancePolicyApi.DTOs.Customer;
    using InsurancePolicyApi.Entities;
    using InsurancePolicyApi.Entities.Enums;
    using InsurancePolicyApi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICloudinaryService _cloudinaryservice;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.InternalStaff)}")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageQuery pq)
        {
            var customers = await _customerService.GetAllAsync(pq);

            return Ok(customers);
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.InternalStaff)}")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();

            return Ok(customers);
        }

        [Authorize]
        [HttpGet("getMyProfile")]
        public async Task<IActionResult> GetMyProfile()
        {
            int userId = int.Parse(User.FindFirst("userid")!.Value);
            var customers = await _customerService.GetMyProfile(userId);

            return Ok(customers);
        }

        // GET: api/customers/5
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [Authorize]
        // GET: api/customers/user/10
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var customer = await _customerService.GetByUserIdAsync(userId);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [Authorize]
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

        [Authorize]
        // PUT: api/customers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProfile(int id, CustomerUpdateRequest customer)
        {
            var updated = await _customerService.UpdateProfileAsync(id, customer);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize]
        [HttpPost("{customerId}/profile-image")]
        public async Task<IActionResult> UploadProfileImage(int customerId, IFormFile file)
        {
            var url = await _customerService.UploadProfileImageAsync(customerId, file);

            if (url == null)
                return NotFound();

            return Ok(new
            {
                profileImageUrl = url
            });
        }
    }
}
