namespace LowPolyBacklogWeb.Models
{
    public class BacklogInfoViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public int HoursPlayed { get; set; }
        public string? ReviewNotes { get; set; }
    }
}