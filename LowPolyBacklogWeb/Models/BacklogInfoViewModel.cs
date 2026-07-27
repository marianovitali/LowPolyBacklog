using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogWeb.Models
{
    public class BacklogInfoViewModel
    {
        public int Id { get; set; }
        public PlayStatus Status { get; set; }
        public int? Rating { get; set; }
        public int HoursPlayed { get; set; }
        public string? ReviewNotes { get; set; }
    }
}