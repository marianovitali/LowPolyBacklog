using LowPolyBacklogWeb.Models;
using System.Text;
using System.Text.Json;

namespace LowPolyBacklogWeb.Services
{
    public class BacklogApiService : IBacklogApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonOptions;

        public BacklogApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        private HttpClient GetClientWithApiKey()
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");
            var apiKey = _configuration["ApiKey"];

            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            }

            return client;
        }

        public async Task<IEnumerable<BacklogItemViewModel>> GetAllBacklogsAsync()
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");
            var response = await client.GetAsync("api/backlogs");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<BacklogItemViewModel>>(content, _jsonOptions)
                       ?? new List<BacklogItemViewModel>();
            }

            return new List<BacklogItemViewModel>();
        }

        public async Task<BacklogItemViewModel?> GetBacklogByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");
            var response = await client.GetAsync($"api/backlogs/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BacklogItemViewModel>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<BacklogItemViewModel?> CreateBacklogAsync(BacklogItemViewModel entry)
        {
            var client = GetClientWithApiKey();
            var jsonContent = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/backlogs", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BacklogItemViewModel>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<bool> UpdateBacklogAsync(int id, BacklogItemViewModel entry)
        {
            var client = GetClientWithApiKey();
            var jsonContent = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/backlogs/{id}", jsonContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBacklogAsync(int id)
        {
            var client = GetClientWithApiKey();
            var response = await client.DeleteAsync($"api/backlogs/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
