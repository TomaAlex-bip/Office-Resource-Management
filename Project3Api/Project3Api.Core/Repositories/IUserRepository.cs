using JetBrains.Annotations;
using Project3Api.Core.Entities;

namespace Project3Api.Core.Repositories
{
    /// <summary>
    /// Repository for User entities
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Gets user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<User?> GetUserAsync(string username);

        /// <summary>
        /// Gets user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<User?> GetUserAsync(string username, string password);

        /// <summary>
        /// Gets user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<User?> GetUserAsync(Guid id);

        /// <summary>
        /// Gets current DeskAllocation of user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<DeskAllocation?> GetCurrentDeskAllocationAsync(Guid userId);

        /// <summary>
        /// Gets DeskAllocation of user in tine intaerval.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<DeskAllocation?> GetSpecifiedDeskAllocationAsync(Guid userId, DateTime from, DateTime until);

        /// <summary>
        /// Gets DeskAllocations of user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<IEnumerable<DeskAllocation>?> GetDeskAllocationsAsync(Guid userId);

        /// <summary>
        /// Gets DeskAllocations of user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<IEnumerable<DeskAllocation>?> GetDeskAllocationsAsync(string username);
    }
}
