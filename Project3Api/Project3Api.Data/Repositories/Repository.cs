using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Project3Api.Core.Repositories;

namespace Project3Api.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        protected readonly ProjectDbContext _dbContext;

        public Repository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync([NotNull] TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete([NotNull] TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public TEntity UpdateAsync([NotNull] TEntity entity)
        {
            var entry = _dbContext.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
            return entity;
        }
    }
}
