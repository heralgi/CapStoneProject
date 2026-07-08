using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task<Customer?> GetByUserIdAsync(int userId);

        Task<Customer> CreateProfileAsync(Customer customer);

        Task<Customer?> UpdateProfileAsync(int id, Customer customer);
    }
}
