using LowPolyBacklogWeb.Models;

namespace LowPolyBacklogWeb.Services
{
    public interface IDashboardApiService
    {
        Task<DashboardSummaryViewModel> GetSummaryAsync();
    }
}
