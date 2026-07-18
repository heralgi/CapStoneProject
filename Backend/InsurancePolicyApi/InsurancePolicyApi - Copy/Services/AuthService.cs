using InsurancePolicyApi.DTOs.Auth;

using InsurancePolicyApi.Entities.Enums;

//using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsurancePolicyApi.Services
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly InsurancePolicyApi.Entities.JWTSettings jwtsettings;
        private readonly PasswordHasher<InsurancePolicyApi.Entities.User> _passwordHasher;

        public AuthService(IAuthRepository repo, IOptions<InsurancePolicyApi.Entities.JWTSettings> options)
        {
            _repo = repo;
            jwtsettings = options.Value;
            _passwordHasher = new PasswordHasher<Entities.User>();
        }
        public async Task<LoginResponse> Login(LoginRequest req)
        {
            LoginResponse res = new LoginResponse { isSuccess = false };
            var user = await _repo.GetUserByEmailAsync(req.Email);
            if (user != null && user.IsActive)
            {
                var result = _passwordHasher.VerifyHashedPassword(
                         user,
                         user.PasswordHash,
                         req.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    res = new LoginResponse
                    {
                        isSuccess = true,
                        UserName = user.FullName,
                        Email = user.Email,
                        UserId = user.Id,
                        Token = "",
                        Role = user.Role.ToString()
                    };
                }
            }
            if (res != null && res.isSuccess)
            {
                res.Token =  GenerateToken(res);
            }
            return res;
        }

        string GenerateToken(LoginResponse res)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(jwtsettings.Key));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Define the Payload
            var claims = new List<Claim>
            {
                new Claim("userid", res.UserId.ToString()),
                new Claim("name", res.UserName),
                new Claim("email", res.Email),
                new Claim("role", res.Role)
            };
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtsettings.Issuer, //take issuer from appsettings
                audience: jwtsettings.Audience, //take audience from appsettings
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            if (await _repo.EmailExistsAsync(dto.Email))
            {
                throw new Exception("Email already exists.");
            }

            var user = new InsurancePolicyApi.Entities.User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, dto.Password),
                MobileNumber = dto.MobileNumber,
                Role = UserRole.Customer,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var customer = new InsurancePolicyApi.Entities.Customer
            {
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                PinCode = dto.PinCode,
                NomineeName = dto.NomineeName,
                NomineeRelation = dto.NomineeRelation,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _repo.RegisterCustomerAsync(user, customer);
        }
    }
}
