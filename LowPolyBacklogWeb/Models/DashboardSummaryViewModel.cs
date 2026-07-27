using LowPolyBacklogApi.DTOs.Dashboard;
using LowPolyBacklogApi.DTOs.Game;

namespace LowPolyBacklogWeb.Models
{
    public class DashboardSummaryViewModel
    {
        // USER METRICS
        public int TotalHoursPlayed { get; set; }
        public int CompletedGames { get; set; }
        public int PendingGames { get; set; }

        // PLAYING NOW
        public List<ActivePlayViewModel> CurrentlyPlaying { get; set; } = new List<ActivePlayViewModel>();

        // RECOMMENDATIONS OF THE MONTH
        public List<GameRecommendationViewModel> MonthlyRecommendations { get; set; } = new List<GameRecommendationViewModel>();
    }
}
