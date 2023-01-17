using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Services;

namespace Project3Api.Services
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LogsResponse> GetLogs(int bulkIndex, int bulkSize = 20,
                                                string? filter = null,
                                                string? order = null, string orderBy = "date")
        {
            List<LogDto> logsResponse = new();
            bool? orderAsc = order == null ? null : true;
            if(order == "desc")
                orderAsc = false;

            Tuple<IEnumerable<Log>, int> logs = await _unitOfWork.LogRepository.GetLogsInBulk(bulkIndex, bulkSize, filter, orderAsc, orderBy);
            foreach(Log log in logs.Item1)
            {
                logsResponse.Add(MapLogToLogDto(log));
            }

            return new()
            {
                LogsCount = logs.Item2,
                Logs = logsResponse
            };
        }

        public async Task LogAsync(string message)
        {
            Log logToAdd = new()
            {
                Message = message,
                DateTime = DateTime.Now
            };

            await _unitOfWork.LogRepository.AddAsync(logToAdd);
            await _unitOfWork.CommitAsync();
        }

        private LogDto MapLogToLogDto(Log log)
        {
            return new()
            {
                DateTime = log.DateTime,
                Message = log.Message
            };
        }
    }
}
