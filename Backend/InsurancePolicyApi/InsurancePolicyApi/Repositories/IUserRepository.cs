

using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface IUserRepository
    {
        /*public Task<IEnumerable<User>> GetAll();
        public Task<User> GetById(int id);
        public Task<User> Add(User user);
        public Task<User> Update(int id, User user);
        public Task<bool> Delete(int id);*/

        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<bool> ActivateAsync(int id);

        Task<bool> DeactivateAsync(int id);
    }
}
