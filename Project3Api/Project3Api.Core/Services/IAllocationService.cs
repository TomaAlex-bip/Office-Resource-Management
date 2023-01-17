using JetBrains.Annotations;
using Project3Api.Core.Dtos;

namespace Project3Api.Core.Services
{
    /// <summary>
    /// Service for Desk and Resource Allocations
    /// </summary>
    public interface IAllocationService
    {
        /// <summary>
        /// Gets all the desk allocations for the specified user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>An IEnumerable of DeskAllocationDto objects. 
        /// If the list has only one item with an ErrorMessage, then this means there was an error. </returns>
        [ItemNotNull]
        Task<IEnumerable<DeskAllocationDto>> GetDeskAllocationsForUser([NotNull] string username);

        /// <summary>
        /// Gets all the desk allocations for the specified user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>An IEnumerable of DeskAllocationDto objects. 
        /// If the list has only one item with an ErrorMessage, then this means there was an error. </returns>
        [ItemNotNull]
        Task<IEnumerable<DeskAllocationDto>> GetDeskAllocationsForUser([NotNull] Guid userId);
    }
}
