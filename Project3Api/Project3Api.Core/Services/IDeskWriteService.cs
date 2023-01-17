using JetBrains.Annotations;
using Project3Api.Core.Dtos;

namespace Project3Api.Core.Services
{
    /// <summary>
    /// Service for desk modifications.
    /// </summary>
    public interface IDeskWriteService
    {
        /// <summary>
        /// Adds a new desk.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gridPosX"></param>
        /// <param name="gridPosY"></param>
        /// <param name="orientation"></param>
        /// <returns>A DeskDto object. If the ErrorMessage is null, everything went well, 
        /// otherwise the ErrorMessage specifies the occured error.</returns>
        [ItemNotNull]
        Task<DeskDto> AddDesk(string name, int gridPosX, int gridPosY, int orientation);

        /// <summary>
        /// Updated an existing desk.
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        /// <param name="newGridPosX"></param>
        /// <param name="newGridPosY"></param>
        /// <param name="newOrientation"></param>
        /// <returns>A DeskDto object. If the ErrorMessage is null, everything went well, 
        /// otherwise the ErrorMessage specifies the occured error.</returns>
        [ItemNotNull]
        Task<DeskDto> UpdateDesk(string oldName, string newName, 
                                 int newGridPosX, int newGridPosY, int newOrientation);

        /// <summary>
        /// Deletes an existing desk.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A DeskDto object. If the ErrorMessage is null, everything went well, 
        /// otherwise the ErrorMessage specifies the occured error.</returns>
        [ItemNotNull]
        Task<DeskDto> DeleteDesk(string name);

        /// <summary>
        /// Reserved a desk for an user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deskName"></param>
        /// <param name="reservedFrom"></param>
        /// <param name="reservedUntil"></param>
        /// <returns>A DeskAllocationDto object. If the ErrorMessage is null, everything went well, 
        /// otherwise the ErrorMessage specifies the occured error.</returns>
        [ItemNotNull]
        Task<DeskAllocationDto> MakeDeskReservation(Guid userId, string deskName, 
                                                    DateTime reservedFrom, DateTime reservedUntil);

    }
}
