using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Services;
using Project3Api.Services.ErrorMessages;

namespace Project3Api.Services
{
    public class AllocationService : IAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AllocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeskAllocationDto>> GetDeskAllocationsForUser(string username)
        {
            List<DeskAllocationDto> deskAllocationsResponse = new();

            IEnumerable<DeskAllocation>? deskAllocations = 
                await _unitOfWork.UserRepository.GetDeskAllocationsAsync(username);
            if(deskAllocations == null)
            {
                deskAllocationsResponse.Add(new DeskAllocationDto
                {
                    Username = username,
                    ErrorMessage = ErrorMessageHelper.GetUserNotFoundError(username)
                }); ;

                return deskAllocationsResponse;
            }

            foreach(var deskAllocation in deskAllocations)
            {
                Desk? desk = await _unitOfWork.DeskRepository.GetDeskAsync(deskAllocation.DeskId);
                if (desk == null)
                    continue;

                DeskAllocationDto deskAllocationDto = new()
                {
                    DeskName = desk.Name,
                    Username = username,
                    ReservedFrom = deskAllocation.ReservedFrom,
                    ReservedUntil = deskAllocation.ReservedUntil
                };

                deskAllocationsResponse.Add(deskAllocationDto);
            }

            return deskAllocationsResponse;
        }

        public async Task<IEnumerable<DeskAllocationDto>> GetDeskAllocationsForUser(Guid userId)
        {
            List<DeskAllocationDto> deskAllocationsResponse = new();

            IEnumerable<DeskAllocation>? deskAllocations =
                await _unitOfWork.UserRepository.GetDeskAllocationsAsync(userId);
            if (deskAllocations == null)
            {
                deskAllocationsResponse.Add(new DeskAllocationDto
                {
                    ErrorMessage = ErrorMessageHelper.GetUserNotFoundError("")
                });

                return deskAllocationsResponse;
            }

            User? user = await _unitOfWork.UserRepository.GetUserAsync(userId);
            if(user == null)
            {
                deskAllocationsResponse.Add(new DeskAllocationDto
                {
                    ErrorMessage = ErrorMessageHelper.GetUserNotFoundError("")
                });

                return deskAllocationsResponse;
            }

            foreach (var deskAllocation in deskAllocations)
            {
                Desk? desk = await _unitOfWork.DeskRepository.GetDeskAsync(deskAllocation.DeskId);
                if (desk == null)
                    continue;

                DeskAllocationDto deskAllocationDto = new()
                {
                    DeskName = desk.Name,
                    Username = user.Username,
                    ReservedFrom = deskAllocation.ReservedFrom,
                    ReservedUntil = deskAllocation.ReservedUntil
                };

                deskAllocationsResponse.Add(deskAllocationDto);
            }

            return deskAllocationsResponse;
        }
    }
}
