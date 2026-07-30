using LowPolyBacklogShared.Enums;

namespace LowPolyBacklogShared.DTOs.Backlog
{
    public class BacklogResponseDto
    {
        public int Id { get; set; }
        public PlayStatus Status { get; set; } = PlayStatus.Pending;
        public int? Rating { get; set; }
        public int HoursPlayed { get; set; } = 0;
        public string? ReviewNotes { get; set; }

        public int GameId { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }

    }
}
