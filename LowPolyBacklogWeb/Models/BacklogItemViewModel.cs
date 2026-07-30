using LowPolyBacklogShared.Enums;

namespace LowPolyBacklogWeb.Models
{
    public class BacklogItemViewModel
    {
        public int Id { get; set; }
        public PlayStatus Status { get; set; }
        public int? Rating { get; set; }
        public int HoursPlayed { get; set; }
        public string? ReviewNotes { get; set; }
        public int GameId { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
    }
}
