using LowPolyBacklogWeb.Models;
using System.Text.Json;

namespace LowPolyBacklogWeb.Services
{
    public class IgdbApiService : IIgdbApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IgdbApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<IgdbSearchResultViewModel>> SearchGamesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<IgdbSearchResultViewModel>();
            }

            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var response = await client.GetAsync($"api/igdb/search?query={Uri.EscapeDataString(query)}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<IgdbSearchResultViewModel>>(
                    jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                return result ?? new List<IgdbSearchResultViewModel>();
            }

            return new List<IgdbSearchResultViewModel>();
        }
    }
}
