using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task<Customer?> GetByUserIdAsync(int userId);

        Task<Customer> CreateProfileAsync(Customer customer);

        Task<Customer> UpdateProfileAsync(Customer customer);
    }
}
