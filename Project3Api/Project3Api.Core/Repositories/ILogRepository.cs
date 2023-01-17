using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;

namespace Project3Api.Core.Repositories
{
    /// <summary>
    /// Repository for logs.
    /// </summary>
    public interface ILogRepository : IRepository<Log>
    {
        Task<Tuple<IEnumerable<Log>, int>> GetLogsInBulk(int bulkIndex, int bulkSize,
                                                         string? filter = null,
                                                         bool? orderAsc = null, string orderBy = "date");

    }
}
