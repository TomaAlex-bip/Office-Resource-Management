namespace Project3Api.Core.Dtos
{
    public class LogsResponse
    {
        public int LogsCount { get; set; }

        public IEnumerable<LogDto> Logs { get; set; }
    }
}
