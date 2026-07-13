using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface ICustomerService
    {
        Task<PagedResponse<Customer>> GetAllAsync(PageQuery pq);

        Task<Customer?> GetByIdAsync(int id);

        Task<Customer?> GetByUserIdAsync(int userId);

        Task<Customer> CreateProfileAsync(Customer customer);

        Task<Customer?> UpdateProfileAsync(int id, Customer customer);
    }
}
