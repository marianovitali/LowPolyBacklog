using LowPolyBacklogShared.DTOs.Dashboard;

namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}
