using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.Customer;
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

        public async Task<PagedResponse<Customer>> GetAllAsync(PageQuery pq)
        {
            return await _customerRepository.GetAllAsync(pq);
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            List<CustomerResponse> responses = new List<CustomerResponse>();

            foreach(Customer customer in customers)
            {
                responses.Add(new CustomerResponse
                {
                    CustomerId = customer.Id,
                    FullName = customer.User.FullName,
                    Email = customer.User.Email,
                    MobileNumber = customer.User.MobileNumber,
                    DateOfBirth = customer.DateOfBirth,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    PinCode = customer.PinCode,
                    NomineeName = customer.NomineeName,
                    NomineeRelation = customer.NomineeRelation
                });
            }
            return responses;
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
