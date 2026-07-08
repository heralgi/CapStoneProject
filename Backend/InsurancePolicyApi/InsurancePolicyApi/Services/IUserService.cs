using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<User> CreateAsync(User user);

        Task<User?> UpdateAsync(int id, User user);

        Task<bool> ActivateAsync(int id);

        Task<bool> DeactivateAsync(int id);
    }
}
