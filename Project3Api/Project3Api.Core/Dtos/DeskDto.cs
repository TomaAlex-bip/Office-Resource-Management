namespace Project3Api.Core.Dtos
{
    public class DeskDto
    {
        public string Name { get; set; }

        public int GridPositionX { get; set; }
        
        public int GridPositionY { get; set; }

        public int Orientation { get; set; }

        public string? OccupyingUser { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? ErrorMessage { get; set; }

    }
}
