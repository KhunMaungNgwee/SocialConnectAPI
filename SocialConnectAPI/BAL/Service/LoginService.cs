using BAL.IService;
using BAL.Shared;
using Microsoft.AspNetCore.Identity;
using MODEL.DTO;
using MODEL.Entity;
using REPOSITORY.UnitOfWork;
using SocialConnectAPI.MODEL.DTO;
using System.ComponentModel.DataAnnotations;

namespace BAL.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<string> _blacklistedTokens = new List<string>();
        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResultDTO> UserLogin(LoginRequestDTO inputUser)
        {
            var user = (await _unitOfWork.IUserRepo
                .GetByCondition(u => u.Email == inputUser.Email))
                .FirstOrDefault();

            if (user == null)
                return new LoginResultDTO { Success = false, Message = "User Not Found" };

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, inputUser.Password);

            if (result == PasswordVerificationResult.Failed)
                return new LoginResultDTO { Success = false, Message = "Invalid Password" };

            var token = TokenGenerator.GenerateCustomerToken(user);

            return new LoginResultDTO
            {
                Success = true,
                Message = "Login Successful",
                Token = token,
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email
                }
            };
        }

       
        public async Task<RegisterResultDTO> Register(RegisterDTO inputUser)
        {
            if (string.IsNullOrWhiteSpace(inputUser.Name) || inputUser.Name.Length > 255)
                return new RegisterResultDTO { Success = false, Message = "Invalid name" };

            if (string.IsNullOrWhiteSpace(inputUser.Email) ||
                !new EmailAddressAttribute().IsValid(inputUser.Email))
                return new RegisterResultDTO { Success = false, Message = "Invalid email" };

            var existingUser = (await _unitOfWork.IUserRepo
                .GetByCondition(u => u.Email == inputUser.Email))
                .FirstOrDefault();

            if (existingUser != null)
                return new RegisterResultDTO { Success = false, Message = "Email already exists" };

            if (string.IsNullOrWhiteSpace(inputUser.Password) || inputUser.Password.Length < 8)
                return new RegisterResultDTO { Success = false, Message = "Password must be at least 8 characters" };

            if (inputUser.Password != inputUser.ConfirmPassword)
                return new RegisterResultDTO { Success = false, Message = "Password confirmation does not match" };

            var user = new User
            {
                Name = inputUser.Name,
                Email = inputUser.Email,
                CreatedAt = DateTime.UtcNow
            };

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, inputUser.Password);

            await _unitOfWork.IUserRepo.Add(user);
            int save = await _unitOfWork.SaveChangesAsync();

            if (save > 0)
            {
                return new RegisterResultDTO
                {
                    Success = true,
                    Message = "User registered successfully",
                    User = user
                };
            }

            return new RegisterResultDTO
            {
                Success = false,
                Message = "Failed to register user"
            };
        }
        public async Task<LogoutResultDTO> UserLogout(string token)
        {
            try
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }
                if (!_blacklistedTokens.Contains(token))
                {
                    _blacklistedTokens.Add(token);
                }

                return new LogoutResultDTO
                {
                    Success = true,
                    Message = "Logout successful"
                };
            }
            catch (Exception ex)
            {
                return new LogoutResultDTO
                {
                    Success = false,
                    Message = $"Logout failed: {ex.Message}"
                };
            }
        }

    }
}
