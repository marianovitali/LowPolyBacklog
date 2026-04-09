namespace LowPolyBacklogApi.Entities
{
    public class BacklogEntry
    {
        public Guid Id { get; set; }

        public PlayStatus Status { get; set; } = PlayStatus.Pending;

        public int? Rating { get; set; }

        public int HoursPlayed { get; set; } = 0;

        public string? ReviewNotes { get; set; }

        public Guid GameId { get; set; }

        // JOIN
        public Game Game { get; set; } = null!;
    }
}
