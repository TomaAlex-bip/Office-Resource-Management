#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Project3Api.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public List<DeskAllocation> DeskAllocations { get; set; }

        public List<ResourceAllocation> ResourceAllocations { get; set; }
    }
}
