using System.Reflection;
using System.Resources;

namespace Project3Api.Services.ErrorMessages
{
    public class ErrorMessageHelper
    {
        private const string ERROR_FILE_NAMESPACE = "Project3Api.Services.ErrorMessages.ErrorTypesMessages";
        private const string USER_NOT_FOUND_ERROR_NAME = "UserNotFound";
        private const string USER_DUPLICATE_ERROR_NAME = "UserDuplicate";
        private const string DESK_NOT_FOUND_ERROR_NAME = "DeskNotFound";
        private const string DESK_DUPLICATE_ERROR_NAME = "DeskDuplicate";
        private const string INVALID_DATE_ERROR_NAME = "InvalidDate";
        private const string OCCUPIED_DESK_ERROR_NAME = "OccupiedDesk";
        private const string ALREADY_RESERVED_DESK_ERROR_NAME = "AlreadyReservedDesk";

        public static string GetUserNotFoundError(string username)
        {
            string message = GetError(USER_NOT_FOUND_ERROR_NAME);
            return message.Replace("{username}", username);
        }

        public static string GetUserDuplicateError(string username)
        {
            string message = GetError(USER_DUPLICATE_ERROR_NAME);
            return message.Replace("{username}", username);
        }

        public static string GetDeskDuplicateError(string deskName)
        {
            string message = GetError(DESK_DUPLICATE_ERROR_NAME);
            return message.Replace("{deskName}", deskName);
        }

        public static string GetDeskNotFoundError(string deskName)
        {
            string message = GetError(DESK_NOT_FOUND_ERROR_NAME);
            return message.Replace("{deskName}", deskName);
        }

        public static string GetInvalidDateError()
        {
            string message = GetError(INVALID_DATE_ERROR_NAME);
            return message;
        }

        public static string GetOccupiedDeskError(string deskName, string username)
        {
            string message = GetError(OCCUPIED_DESK_ERROR_NAME);
            return message.Replace("{deskName}", deskName)
                          .Replace("{username}", username);
        }

        public static string GetAlreadyReservedDeskError(string deskName, DateTime from, DateTime until)
        {
            string message = GetError(ALREADY_RESERVED_DESK_ERROR_NAME);
            return message.Replace("{deskName}", deskName)
                          .Replace("{from}", from.ToShortDateString())
                          .Replace("{until}", until.ToShortDateString());
        }

        private static string GetError(string name)
        {
            ResourceManager rm = new(ERROR_FILE_NAMESPACE, Assembly.GetExecutingAssembly());
            string? message = rm.GetString(name);
            if (message == null)
                return string.Empty;

            return message;
        }
    }
}
