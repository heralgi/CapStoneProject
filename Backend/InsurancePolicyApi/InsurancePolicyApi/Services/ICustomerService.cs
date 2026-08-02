using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.Customer;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface ICustomerService
    {
        Task<PagedResponse<Customer>> GetAllAsync(PageQuery pq);
        Task<IEnumerable<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse> GetMyProfile(int userId);

        Task<Customer?> GetByIdAsync(int id);

        Task<Customer?> GetByUserIdAsync(int userId);

        Task<Customer> CreateProfileAsync(Customer customer);

        Task<Customer?> UpdateProfileAsync(int id, CustomerUpdateRequest customer);

        Task<string?> UploadProfileImageAsync(int customerId, IFormFile file);
    }
}
