using Azure.Core;
using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.DTOs.User;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Entities.Enums;
using InsurancePolicyApi.Repositories;
using Microsoft.AspNetCore.Identity;

namespace InsurancePolicyApi.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<PagedResponse<User>> GetAllAsync(PageQuery pq)
        {
            return await _userRepository.GetAllAsync(pq);
        }
        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return MapToResponseList(users);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> CreateAsync(User user)
        {
            // Business Rules

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if (existingUser != null)
                throw new Exception("Email already exists.");

            user.IsActive = true;
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            return await _userRepository.CreateAsync(user);
        }

        public async Task<UserResponse?> CreateAdminORInternalStaffAsync(CreateUserRequest request)
        {
            if (request.Role == UserRole.Customer)
                throw new Exception("Admin cannot create Customer.");

            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("Email already exists.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                Role = request.Role,
                IsActive = true
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var created = await _userRepository.CreateAsync(user);

            return MapToResponse(created);
        }

        public async Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
                return null;

            existingUser.FullName = user.FullName;
            existingUser.MobileNumber = user.MobileNumber;
            existingUser.Role = user.Role;

            var userResponse = await _userRepository.UpdateAsync(existingUser);
            return MapToResponse(userResponse);
        }

        public async Task<bool> ActivateAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            if (user.IsActive)
                return true;

            return await _userRepository.ActivateAsync(id);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            if (!user.IsActive)
                return true;

            return await _userRepository.DeactivateAsync(id);
        }

        private static UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            };
        }
        private static IEnumerable<UserResponse> MapToResponseList(IEnumerable<User> users)
        {
            List<UserResponse> userResponses = new List<UserResponse>();
            foreach(User user in users)
            {
                userResponses.Add(
                        new UserResponse
                        {
                            UserId = user.Id,
                            FullName = user.FullName,
                            Email = user.Email,
                            MobileNumber = user.MobileNumber,
                            Role = user.Role.ToString(),
                            IsActive = user.IsActive,
                            CreatedDate = user.CreatedDate
                        }
                );
            }
            return userResponses;
        }
    }
}
