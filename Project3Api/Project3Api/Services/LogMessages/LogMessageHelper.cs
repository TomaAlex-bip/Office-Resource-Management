namespace Project3Api.Services.LogMessages
{
    public static class LogMessageHelper
    {
        public static string GetRegistrationLogMessage(string username, string? error = null)
        {
            string status = error == null ? "Success: " : $"Fail (error: {error}): ";
            return $"{status} Register new user with username: {username}";
        }

        public static string GetDeskReservationLogMessage(string username, string deskName, string? error = null)
        {
            string status = error == null ? "Success: " : $"Fail (error: {error}): ";
            return $"{status} User: [{username}] reserved desk: [{deskName}]";
        }
    }
}
