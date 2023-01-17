#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Project3Api.Core.Entities
{
    public class Log
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }

    }
}
