using Microsoft.EntityFrameworkCore;
using Project3Api.Core.Entities;
using Project3Api.Core.Repositories;

namespace Project3Api.Data.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Tuple<IEnumerable<Log>, int>> GetLogsInBulk(int bulkIndex, int bulkSize,
                                                                      string? filter = null,
                                                                      bool? orderAsc = null, string orderBy = "date")
        {
            IQueryable<Log> processedLogs;

            if(filter != null)
            {
                processedLogs = _dbContext.Logs.Where(x => x.Message.ToLower().Contains(filter.ToLower()));

                processedLogs = GetLogsInOrder(processedLogs, orderBy, orderAsc);
            }
            else
            {
                processedLogs = _dbContext.Logs;

                processedLogs = GetLogsInOrder(processedLogs, orderBy, orderAsc);
            }

            int totalLogs = await processedLogs.CountAsync();
            IQueryable<Log> chunkLogs = processedLogs.Skip(bulkIndex * bulkSize).Take(bulkSize);

            IEnumerable<Log> processedChunk = await chunkLogs.ToListAsync();

            return new(processedChunk, totalLogs);
        }

        private IQueryable<Log> GetLogsInOrder(IQueryable<Log> unorderedLogs, string orderBy, bool? orderAsc)
        {
            if(orderAsc == null)
            {
                return unorderedLogs;
            }
            
            switch (orderBy)
            {
                case "date":
                    if (orderAsc == true)
                        return unorderedLogs.OrderBy(x => x.DateTime);
                    else
                        return unorderedLogs.OrderByDescending(x => x.DateTime);

                case "message":
                    if (orderAsc == true)
                        return unorderedLogs.OrderBy(x => x.Message);
                    else
                        return unorderedLogs.OrderByDescending(x => x.Message);
            }

            return unorderedLogs;
        }

    }
}
