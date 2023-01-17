using Project3Api.Core;
using Project3Api.Core.Repositories;
using Project3Api.Data.Repositories;

namespace Project3Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _dbContext;

        public IDeskRepository DeskRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IResourceRepository ResourceRepository { get; private set; }

        public ILogRepository LogRepository { get; private set; }

        public UnitOfWork(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;

            DeskRepository = new DeskRepository(dbContext);
            UserRepository = new UserRepository(dbContext);
            ResourceRepository = new ResourceRepository(dbContext);
            LogRepository = new LogRepository(dbContext);
        }


        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
