using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.Customer;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, 
            ICloudinaryService cloudinaryService, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _cloudinaryService = cloudinaryService;
            _userRepository = userRepository;
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

        public async Task<CustomerResponse> GetMyProfile(int userId)
        {
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null)
                throw new Exception("Customer does not exists.");

            var response = new CustomerResponse()
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
            };

            return response;
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

        public async Task<Customer?> UpdateProfileAsync(int id, CustomerUpdateRequest customer)
        {
            var existing = await _customerRepository.GetByIdAsync(id);

            if (existing == null)
                throw new Exception("Customer not Found.");

            User user = existing.User;
            user.FullName = customer.FullName;
            user.MobileNumber = customer.MobileNumber;

            await _userRepository.UpdateAsync(user);
            
            existing.DateOfBirth = customer.DateOfBirth;
            existing.Address = customer.Address;
            existing.City = customer.City;
            existing.State = customer.State;
            existing.PinCode = customer.PinCode;
            existing.NomineeName = customer.NomineeName;
            existing.NomineeRelation = customer.NomineeRelation;

            return await _customerRepository.UpdateProfileAsync(existing);
        }

        public async Task<string?> UploadProfileImageAsync(int customerId, IFormFile file)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                return null;

            var imageUrl = await _cloudinaryService.UploadImageAsync(file);

            customer.ImageUrl = imageUrl;

            await _customerRepository.UpdateProfileAsync(customer);

            return imageUrl;
        }
    }
}
