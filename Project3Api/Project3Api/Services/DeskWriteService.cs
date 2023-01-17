using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Services;
using Project3Api.Services.ErrorMessages;

namespace Project3Api.Services
{
    public class DeskWriteService : IDeskWriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeskWriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeskDto> AddDesk(string name, int gridPosX, int gridPosY, int orientation)
        {
            DeskDto deskResponse = new()
            {
                Name = name,
                GridPositionX = gridPosX,
                GridPositionY = gridPosY,
                Orientation = orientation,
            };

            Desk? deskDuplicate = await _unitOfWork.DeskRepository.GetDeskAsync(name);
            if(deskDuplicate != null)
            {
                deskResponse.ErrorMessage = ErrorMessageHelper.GetDeskDuplicateError(name);
                return deskResponse;
            }

            Desk deskToBeAdded = new()
            {
                Name = name,
                GridPositionX = gridPosX,
                GridPositionY = gridPosY,
                Orientation = orientation
            };

            await _unitOfWork.DeskRepository.AddAsync(deskToBeAdded);
            await _unitOfWork.CommitAsync();

            return deskResponse;
        }

        public async Task<DeskDto> UpdateDesk(string oldName, string newName, 
                                              int newGridPosX, int newGridPosY, 
                                              int newOrientation)
        {
            DeskDto deskResponse = new()
            {
                Name = newName,
                GridPositionX = newGridPosX,
                GridPositionY = newGridPosY,
                Orientation = newOrientation
            };

            Desk? deskToUpdate = await _unitOfWork.DeskRepository.GetDeskAsync(oldName);
            if (deskToUpdate == null)
            {
                deskResponse.ErrorMessage = ErrorMessageHelper.GetDeskNotFoundError(oldName);
                return deskResponse;
            }

            deskToUpdate.Name = newName;
            deskToUpdate.GridPositionX = newGridPosX;
            deskToUpdate.GridPositionY = newGridPosY;
            deskToUpdate.Orientation = newOrientation;

            _unitOfWork.DeskRepository.UpdateAsync(deskToUpdate);
            await _unitOfWork.CommitAsync();

            return deskResponse;
        }

        public async Task<DeskDto> DeleteDesk(string name)
        {
            DeskDto deskResponse = new()
            {
                Name = name
            };

            Desk? deskToDelete = await _unitOfWork.DeskRepository.GetDeskAsync(name);
            if(deskToDelete == null)
            {
                deskResponse.ErrorMessage = ErrorMessageHelper.GetDeskNotFoundError(name);

                return deskResponse;
            }

            deskResponse.Name = deskToDelete.Name;
            deskResponse.GridPositionX = deskToDelete.GridPositionX;
            deskResponse.GridPositionY = deskToDelete.GridPositionY;
            deskResponse.Orientation = deskToDelete.Orientation;

            _unitOfWork.DeskRepository.Delete(deskToDelete);
            await _unitOfWork.CommitAsync();

            return deskResponse;
        }

        public async Task<DeskAllocationDto> MakeDeskReservation(Guid userId, string deskName, 
                                                                 DateTime reservedFrom, DateTime reservedUntil)
        {
            DeskAllocationDto deskAllocationResponse = new()
            {
                DeskName = deskName,
                ReservedFrom = reservedFrom,
                ReservedUntil = reservedUntil
            };

            User? user = await _unitOfWork.UserRepository.GetUserAsync(userId);
            if (user == null)
            {
                deskAllocationResponse.ErrorMessage = ErrorMessageHelper.GetUserNotFoundError("");
                return deskAllocationResponse;
            }
            deskAllocationResponse.Username = user.Username;

            if (reservedFrom < DateTime.Today.Date)
            {
                deskAllocationResponse.ErrorMessage = ErrorMessageHelper.GetInvalidDateError();
                return deskAllocationResponse;
            }

            Desk? deskReserved = await _unitOfWork.DeskRepository.GetDeskAsync(deskName);
            if(deskReserved == null)
            {
                deskAllocationResponse.ErrorMessage = ErrorMessageHelper.GetDeskNotFoundError(deskName);
                return deskAllocationResponse;
            }

            DeskAllocation? deskAllocationDuplicate = await _unitOfWork.DeskRepository
                .GetSpecifiedDeskAllocationAsync(deskReserved.Id, reservedFrom, reservedUntil);
            if (deskAllocationDuplicate != null)
            {
                deskAllocationResponse.ErrorMessage = ErrorMessageHelper.GetOccupiedDeskError(deskName, deskAllocationResponse.Username);
                return deskAllocationResponse;
            }

            deskAllocationDuplicate = await _unitOfWork.UserRepository
                .GetSpecifiedDeskAllocationAsync(user.Id, reservedFrom, reservedUntil);
            if(deskAllocationDuplicate != null)
            {
                deskAllocationResponse.ErrorMessage = ErrorMessageHelper.GetAlreadyReservedDeskError(deskAllocationDuplicate.Desk.Name, reservedFrom, reservedUntil);
                return deskAllocationResponse;
            }

            DeskAllocation deskAllocation = new()
            {
                User = user,
                Desk = deskReserved,
                ReservedFrom = reservedFrom,
                ReservedUntil = reservedUntil
            };

            deskReserved.DeskAllocations.Add(deskAllocation);
            user.DeskAllocations.Add(deskAllocation);
            await _unitOfWork.CommitAsync();

            return deskAllocationResponse;
        }
    }
}
