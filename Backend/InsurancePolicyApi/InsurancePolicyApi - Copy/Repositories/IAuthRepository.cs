using InsurancePolicyApi.DTOs.Auth;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IAuthRepository
    {
        LoginResponse Login(LoginRequest user);
        Task RegisterCustomerAsync(User user, Customer customer);
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
