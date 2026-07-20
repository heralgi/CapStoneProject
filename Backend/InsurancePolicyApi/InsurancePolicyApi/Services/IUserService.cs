using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.User;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IUserService
    {
        Task<PagedResponse<User>> GetAllAsync(PageQuery pq);
        Task<IEnumerable<UserResponse>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<User> CreateAsync(User user);

        Task<UserResponse> CreateAdminORInternalStaffAsync(CreateUserRequest user);

        Task<User?> UpdateAsync(int id, User user);

        Task<bool> ActivateAsync(int id);

        Task<bool> DeactivateAsync(int id);
    }
}
