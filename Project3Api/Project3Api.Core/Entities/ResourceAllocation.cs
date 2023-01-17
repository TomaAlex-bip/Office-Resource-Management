#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Project3Api.Core.Entities
{
    public class ResourceAllocation
    {
        public Guid Id { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public Resource Resource { get; set; }
        public Guid ResourceId { get; set; }

        public DateTime AllocatedFrom { get; set; }

        public DateTime? AllocatedUntil { get; set; }
    }
}
