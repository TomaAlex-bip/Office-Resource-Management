using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Services;

namespace Project3Api.Services
{
    public class DeskReadService : IDeskReadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeskReadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeskDto>> GetAllDesksAsync()
        {
            IEnumerable<Desk> desks = await _unitOfWork.DeskRepository.GetAllDesksAsync();

            List<DeskDto> desksDto = new();
            foreach (var desk in desks)
            {
                var deskAllocation = await _unitOfWork.DeskRepository
                    .GetCurrentDeskAllocationAsync(desk.Id);

                var deskDto = new DeskDto
                {
                    Name = desk.Name,
                    GridPositionX = desk.GridPositionX,
                    GridPositionY = desk.GridPositionY,
                    Orientation = desk.Orientation,
                    OccupyingUser = deskAllocation?.User.Username,
                    StartDate = deskAllocation?.ReservedFrom.Date,
                    EndDate = deskAllocation?.ReservedUntil.Date,
                };

                desksDto.Add(deskDto);
            }

            return desksDto;
        }

        public Task<DeskDto> GetDesk(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DeskDto> GetDesk(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeskDto>> GetDesksOccupiedByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeskDto>> GetDesksOccupiedByUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeskDto>> GetFreeDesks()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeskDto>> GetOccupiedDesks()
        {
            throw new NotImplementedException();
        }
    }
}
