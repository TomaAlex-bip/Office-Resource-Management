using JetBrains.Annotations;
using Project3Api.Core.Dtos;

namespace Project3Api.Core.Services
{
    /// <summary>
    /// Service for user info and registration
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers a new User.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ItemNotNull]
        Task<UserDto> RegisterUserAsync(string username, string password);

        /// <summary>
        /// Gets info about an user based of the access token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ItemNotNull]
        Task<UserDto> GetUserAsync(Guid userId);
    }
}
