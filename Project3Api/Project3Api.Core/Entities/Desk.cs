#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Project3Api.Core.Entities
{
    public class Desk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public int GridPositionX { get; set; }
        
        public int GridPositionY { get; set; }

        public int Orientation { get; set; }
        
        public List<DeskAllocation> DeskAllocations { get; set; }
    }
}
