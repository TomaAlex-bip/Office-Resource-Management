using Project3Api.Core.Dtos;

namespace Project3Api.Core.Services
{
    public interface ILogService
    {
        Task LogAsync(string message);

        Task<LogsResponse> GetLogs(int bulkIndex, int bulkSize = 20, 
                                   string? filter=null, 
                                   string? order=null, string orderBy="date");
    }
}
