using LowPolyBacklogWeb.Models;
using System.Text.Json;
using System.Net.Http.Json;

namespace LowPolyBacklogWeb.Services
{
    public class GameApiService : IGameApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public GameApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<PagedResponse<GameViewModel>> GetGamesAsync(GameFilterViewModel filters)
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var queryParams = new List<string> { $"pageNumber={filters.PageNumber}" };


            if (!string.IsNullOrEmpty(filters.Title)) queryParams.Add($"title={Uri.EscapeDataString(filters.Title)}");
            if (!string.IsNullOrEmpty(filters.Genre)) queryParams.Add($"genre={Uri.EscapeDataString(filters.Genre)}");
            if (filters.Year.HasValue) queryParams.Add($"year={filters.Year.Value}");

            var queryString = string.Join("&", queryParams);
            var response = await client.GetAsync($"api/games?{queryString}"); 
            
            if (!response.IsSuccessStatusCode)
            {
                return new PagedResponse<GameViewModel>();
            }

            var result = await response.Content.ReadFromJsonAsync<PagedResponse<GameViewModel>>();

            return result ?? new PagedResponse<GameViewModel>();
        }

        public async Task<GameDetailsViewModel?> GetGameByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var response = await client.GetAsync($"api/games/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<GameDetailsViewModel>();

            return result;
        }

        public async Task<bool> UpdateGameAsync(int id, GameUpdateViewModel updatedGame)
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var apiKey = _configuration["ApiKey"];

            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            }

            var response = await client.PutAsJsonAsync($"api/games/{id}", updatedGame);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GenreViewModel>> GetAllGenresAsync()
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var response = await client.GetAsync("api/genres");

            if (!response.IsSuccessStatusCode)
            {
                return new List<GenreViewModel>();
            }

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GenreViewModel>>();

            return result ?? new List<GenreViewModel>();
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var apiKey = _configuration["ApiKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            }

            var response = await client.DeleteAsync($"api/games/{id}");

            return response.IsSuccessStatusCode;
        }

    }
}
