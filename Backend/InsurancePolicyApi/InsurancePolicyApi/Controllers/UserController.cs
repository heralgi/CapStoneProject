using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [Authorize(Roles=nameof(UserRole.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]PageQuery pq)
        {
            var users = await _userService.GetAllAsync(pq);

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }

        // GET: api/users/email/test@gmail.com
        [HttpGet("email/{email}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Create(User user)
        {
            var createdUser = await _userService.CreateAdminORInternalStaffAsync(user);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdUser.Id },
                createdUser);
        }

        // PUT: api/users/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Update(int id, User user)
        {
            var updatedUser = await _userService.UpdateAsync(id, user);

            if (updatedUser == null)
                return NotFound(new { message = "User not found." });

            return Ok(updatedUser);
        }

        // PUT: api/users/activate/5
        [HttpPut("activate/{id:int}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _userService.ActivateAsync(id);

            if (!result)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "User activated successfully." });
        }

        // PUT: api/users/deactivate/5
        [HttpPut("deactivate/{id:int}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _userService.DeactivateAsync(id);

            if (!result)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "User deactivated successfully." });
        }
    }
}
