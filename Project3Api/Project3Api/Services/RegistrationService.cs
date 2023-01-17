using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Security;
using Project3Api.Core.Services;
using Project3Api.Services.ErrorMessages;

namespace Project3Api.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            UserDto userResponse = new();

            User? user = await _unitOfWork.UserRepository.GetUserAsync(userId);
            if(user != null)
            {
                userResponse.Username = user.Username;
                userResponse.Role = user.Role;
            }
            else
            {
                userResponse.ErrorMessage = ErrorMessageHelper.GetUserNotFoundError("");
            }

            return userResponse;
        }

        public async Task<UserDto> RegisterUserAsync(string username, string password)
        {
            // password = Cryptography.HashString(password);

            UserDto userResponse = new()
            {
                Username = username
            };

            User? userDuplicate = await _unitOfWork.UserRepository.GetUserAsync(username);
            if (userDuplicate != null)
            {
                userResponse.ErrorMessage = ErrorMessageHelper.GetUserDuplicateError(username);
                return userResponse;
            }

            User userToBeAdded = new()
            {
                Username = username,
                Password = password,
                Role = 0
            };

            await _unitOfWork.UserRepository.AddAsync(userToBeAdded);
            await _unitOfWork.CommitAsync();

            return userResponse;
        }

        
    }
}
