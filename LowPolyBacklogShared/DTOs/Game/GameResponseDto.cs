namespace LowPolyBacklogShared.DTOs.Game
{
    public class GameResponseDto
    {
        public int Id { get; set; }
        public int? IgdbId { get; set; }
        public required string Title { get; set; }
        public string? Synopsis { get; set; }
        public int ReleaseYear { get; set; }
        public string? Developer { get; set; }
        public string? CoverImageUrl { get; set; }
        public int DiscCount { get; set; } = 1;

        public List<GenreResponseDto> Genres { get; set; } = new();

        //public List<string> Genres { get; set; } = [];
    }
}
