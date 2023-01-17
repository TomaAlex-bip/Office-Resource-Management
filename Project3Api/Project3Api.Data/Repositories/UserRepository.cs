using Microsoft.EntityFrameworkCore;
using Project3Api.Core.Entities;
using Project3Api.Core.Repositories;

namespace Project3Api.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _dbContext.Users
                .Include(x => x.DeskAllocations)
                .Include(x => x.ResourceAllocations)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            return await _dbContext.Users
                .Include(x => x.DeskAllocations)
                .Include(x => x.ResourceAllocations)
                .FirstOrDefaultAsync(x => x.Username == username && 
                                     x.Password == password);
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DeskAllocation?> GetCurrentDeskAllocationAsync(Guid userId)
        {
            var user = await _dbContext.Users
                .Include(x => x.DeskAllocations)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user?.DeskAllocations?
                .FirstOrDefault(x => x.UserId == userId &&
                                x.ReservedFrom >= DateTime.Today &&
                                x.ReservedUntil <= DateTime.Today);
        }

        public async Task<DeskAllocation?> GetSpecifiedDeskAllocationAsync(Guid userId, DateTime from, DateTime until)
        {
            var user = await _dbContext.Users
                .Include(x => x.DeskAllocations)
                .ThenInclude(x => x.Desk)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user?.DeskAllocations?
                .FirstOrDefault(x => x.UserId == userId &&
                                x.ReservedFrom >= from &&
                                x.ReservedUntil <= until);
        }

        public async Task<IEnumerable<DeskAllocation>?> GetDeskAllocationsAsync(Guid userId)
        {
            User? user = await _dbContext.Users
                .Include(x => x.DeskAllocations)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return null;

            return user.DeskAllocations;
        }

        public async Task<IEnumerable<DeskAllocation>?> GetDeskAllocationsAsync(string username)
        {
            User? user = await _dbContext.Users
                .Include(x => x.DeskAllocations)
                .FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return null;

            return user.DeskAllocations.OrderBy(x => x.ReservedFrom);
        }

    }
}
