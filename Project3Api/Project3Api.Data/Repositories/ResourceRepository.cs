using Project3Api.Core.Entities;
using Project3Api.Core.Repositories;

namespace Project3Api.Data.Repositories
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }


    }
}
