using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer?> GetByUserIdAsync(int userId)
        {
            return await _customerRepository.GetByUserIdAsync(userId);
        }

        public async Task<Customer> CreateProfileAsync(Customer customer)
        {
            var existing = await _customerRepository.GetByUserIdAsync(customer.UserId);

            if (existing != null)
                throw new Exception("Customer profile already exists.");

            return await _customerRepository.CreateProfileAsync(customer);
        }

        public async Task<Customer?> UpdateProfileAsync(int id, Customer customer)
        {
            var existing = await _customerRepository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.DateOfBirth = customer.DateOfBirth;
            existing.Address = customer.Address;

            return await _customerRepository.UpdateProfileAsync(existing);
        }
    }
}
