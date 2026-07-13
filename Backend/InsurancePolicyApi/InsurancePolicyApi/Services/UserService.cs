using InsurancePolicyApi.DTOs.Common;
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

        public async Task<User?> CreateAdminORInternalStaffAsync(User user)
        {
            if(user.Role == UserRole.Customer)
            {
                throw new Exception("Admin cannot create Customer.");
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if (existingUser != null)
                throw new Exception("Email already exists.");

            return await _userRepository.CreateAsync(user);
        }

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
                return null;

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            return await _userRepository.UpdateAsync(existingUser);
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
    }
}
