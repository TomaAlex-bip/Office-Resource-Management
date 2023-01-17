#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Project3Api.Core.Entities
{
    public class DeskAllocation
    {
        public Guid Id { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public Desk Desk { get; set; }
        public Guid DeskId { get; set; }

        public DateTime ReservedFrom { get; set; }

        public DateTime ReservedUntil { get; set; }
    }
}
