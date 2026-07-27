namespace LowPolyBacklogWeb.Models
{
    public class GameRecommendationViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
    }
}
