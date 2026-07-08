using InsurancePolicyApi.DTOs.Auth;

namespace InsurancePolicyApi.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest req);
        Task RegisterAsync(RegisterRequestDto dto);
    }
}
