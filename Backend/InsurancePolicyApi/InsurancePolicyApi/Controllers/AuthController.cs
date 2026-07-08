using InsurancePolicyApi.DTOs.Auth;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            
            if (ModelState.IsValid)
            {
                var res = await _service.Login(req);
                if (res.isSuccess)
                {
                    return Ok(res);
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            try
            {
                await _service.RegisterAsync(dto);

                return Ok(new
                {
                    Success = true,
                    Message = "Customer registered successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
