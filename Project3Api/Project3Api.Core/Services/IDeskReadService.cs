using JetBrains.Annotations;
using Project3Api.Core.Dtos;

namespace Project3Api.Core.Services
{
    /// <summary>
    /// Service for desk query.
    /// </summary>
    public interface IDeskReadService
    {
        /// <summary>
        /// Gets all the desks available in the database.
        /// </summary>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<DeskDto>> GetAllDesksAsync();

        Task<DeskDto> GetDesk(int id);

        Task<DeskDto> GetDesk(string name);

        Task<IEnumerable<DeskDto>> GetDesksOccupiedByUser(int userId);

        Task<IEnumerable<DeskDto>> GetDesksOccupiedByUser(string username);

        Task<IEnumerable<DeskDto>> GetFreeDesks();

        Task<IEnumerable<DeskDto>> GetOccupiedDesks();
    }
}
