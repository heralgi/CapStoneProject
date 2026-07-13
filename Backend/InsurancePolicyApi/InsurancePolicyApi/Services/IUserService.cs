using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IUserService
    {
        Task<PagedResponse<User>> GetAllAsync(PageQuery pq);

        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<User> CreateAsync(User user);

        Task<User> CreateAdminORInternalStaffAsync(User user);

        Task<User?> UpdateAsync(int id, User user);

        Task<bool> ActivateAsync(int id);

        Task<bool> DeactivateAsync(int id);
    }
}
