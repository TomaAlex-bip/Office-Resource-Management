using JetBrains.Annotations;

namespace Project3Api.Core.Services
{
    /// <summary>
    /// Service for reading token.
    /// </summary>
    public interface ITokenReaderService
    {
        /// <summary>
        /// Gets the user id from the token string.
        /// </summary>
        /// <param name="token">The token string in full format ("Bearer xxxxx.xxxxx.xxxxx")</param>
        /// <returns>The user id of the user who emmited the token. If there are no claims for it, it will return -1, which is an ivalid id.</returns>
        [ItemNotNull]
        Guid GetUserId([CanBeNull] string token);
    }
}
