using LowPolyBacklogWeb.Models;

namespace LowPolyBacklogWeb.Services
{
    public class DashboardApiService : IDashboardApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DashboardSummaryViewModel> GetSummaryAsync()
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");
            var response = await client.GetFromJsonAsync<DashboardSummaryViewModel>("api/dashboard/summary");

            return response ?? new DashboardSummaryViewModel();
        }
    }
}
