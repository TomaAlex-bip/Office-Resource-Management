using Microsoft.EntityFrameworkCore;
using Project3Api.Core.Entities;
using Project3Api.Core.Repositories;

namespace Project3Api.Data.Repositories
{
    public class DeskRepository : Repository<Desk>, IDeskRepository
    {
        public DeskRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Desk>> GetAllDesksAsync()
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .ThenInclude(x => x.User)
                .ToListAsync();
        }

        public async Task<Desk?> GetDeskAsync(Guid id)
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Desk?> GetDeskAsync(string name)
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<DeskAllocation?> GetCurrentDeskAllocationAsync(Guid deskId)
        {
            var desk = await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == deskId);

            return desk?.DeskAllocations?
                .FirstOrDefault(x => x.DeskId == deskId &&
                                x.ReservedFrom >= DateTime.Today &&
                                x.ReservedUntil <= DateTime.Today);
        }

        public async Task<DeskAllocation?> GetSpecifiedDeskAllocationAsync(Guid deskId, DateTime from, DateTime until)
        {
            var desk = await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == deskId);

            return desk?.DeskAllocations?
                .FirstOrDefault(x => x.DeskId == deskId &&
                                x.ReservedFrom >= from &&
                                x.ReservedUntil <= until);
        }

        public async Task<IEnumerable<Desk>> GetDesksOccupiedByUserAsync(Guid userId)
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .Where(x => x.DeskAllocations.Any(x => x.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Desk>> GetDesksOccupiedByUserAsync(string username)
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .ThenInclude(x => x.User)
                .Where(x => x.DeskAllocations.Any(x => x.User.Username == username))
                .ToListAsync();
        }

        public async Task<IEnumerable<Desk>> GetFreeDesksAsync()
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .Where(x => !x.DeskAllocations.Any(x => x.ReservedFrom < DateTime.Today &&
                                                        x.ReservedUntil > DateTime.Today))
                .ToListAsync();
        }

        public async Task<IEnumerable<Desk>> GetOccupiedDesksAsync()
        {
            return await _dbContext.Desks
                .Include(x => x.DeskAllocations)
                .Where(x => x.DeskAllocations.Any(x => x.ReservedFrom < DateTime.Today &&
                                                       x.ReservedUntil > DateTime.Today))
                .ToListAsync();
        }

        
    }
}
