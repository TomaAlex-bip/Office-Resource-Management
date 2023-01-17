using Project3Api.Core.Repositories;

namespace Project3Api.Core
{
    /// <summary>
    /// Orchestrator for repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDeskRepository DeskRepository { get; } 

        IUserRepository UserRepository { get; }

        IResourceRepository ResourceRepository { get; }

        ILogRepository LogRepository { get; }

        /// <summary>
        /// Saves the changes made to the database
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}