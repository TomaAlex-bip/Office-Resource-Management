using JetBrains.Annotations;
using Project3Api.Core.Entities;

namespace Project3Api.Core.Repositories
{
    /// <summary>
    /// Repository for Desk entities.
    /// </summary>
    public interface IDeskRepository : IRepository<Desk>
    {
        /// <summary>
        /// Gets all the desks.
        /// </summary>
        /// <returns>IEnumerable of Desks</returns>
        [ItemNotNull]
        Task<IEnumerable<Desk>> GetAllDesksAsync();

        /// <summary>
        /// Gets a desk.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Desk if found, or null if not found.</returns>
        [ItemCanBeNull]
        Task<Desk?> GetDeskAsync(Guid id);

        /// <summary>
        /// Gets a desk.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Desk if found, or null if not found.</returns>
        [ItemCanBeNull]
        Task<Desk?> GetDeskAsync(string name);

        /// <summary>
        /// Gets occupied desks by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<Desk>> GetDesksOccupiedByUserAsync(Guid userId);

        /// <summary>
        /// Gets occupied desks by user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<Desk>> GetDesksOccupiedByUserAsync(string username);

        /// <summary>
        /// Gets free desks.
        /// </summary>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<Desk>> GetFreeDesksAsync();

        /// <summary>
        /// Gets occupied desks
        /// </summary>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<Desk>> GetOccupiedDesksAsync();

        /// <summary>
        /// Gets a DeskAllocation.
        /// </summary>
        /// <param name="deskId"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<DeskAllocation?> GetCurrentDeskAllocationAsync(Guid deskId);

        /// <summary>
        /// Gets a DeskAllocation.
        /// </summary>
        /// <param name="deskId"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<DeskAllocation?> GetSpecifiedDeskAllocationAsync(Guid deskId, DateTime from, DateTime until);

    }
}
