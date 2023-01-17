#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Project3Api.Core.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public List<ResourceAllocation> ResourceAllocations { get; set; }
    }
}
