using JetBrains.Annotations;

namespace Project3Api.Core.Repositories
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds a new entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] TEntity entity);

        /// <summary>
        /// Gets all the entities from the database
        /// </summary>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Update an entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ItemNotNull]
        TEntity UpdateAsync([NotNull] TEntity entity);

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete([NotNull] TEntity entity);
    }
}
