namespace Project3Api.Core.Dtos
{
    public class DeskAllocationDto
    {
        public string DeskName { get; set; }

        public string Username { get; set; }

        public DateTime ReservedFrom { get; set; }

        public DateTime ReservedUntil { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
