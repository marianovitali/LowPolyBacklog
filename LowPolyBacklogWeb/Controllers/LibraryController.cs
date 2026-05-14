using LowPolyBacklogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LowPolyBacklogWeb.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LibraryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string? title, string? genre, int? year, int pageNumber = 1)
        {
            ViewData["CurrentTitle"] = title;
            ViewData["CurrentGenre"] = genre;
            ViewData["CurrentYear"] = year;

            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var queryParams = new List<string> { $"pageNumber={pageNumber}" };

            if (!string.IsNullOrEmpty(title)) queryParams.Add($"title={Uri.EscapeDataString(title)}");
            if (!string.IsNullOrEmpty(genre)) queryParams.Add($"genre={Uri.EscapeDataString(genre)}");
            if (year.HasValue) queryParams.Add($"year={year.Value}");

            var queryString = string.Join("&", queryParams);

            var response = await client.GetAsync($"api/games?{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var pagedResult = JsonSerializer.Deserialize<PagedResponse<GameViewModel>>(
                    jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return View(pagedResult ?? new PagedResponse<GameViewModel>());
            }

            return View(new PagedResponse<GameViewModel>());
        }
    }
}
