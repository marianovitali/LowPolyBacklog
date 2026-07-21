using LowPolyBacklogWeb.Models;
using LowPolyBacklogWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogWeb.Controllers
{
    public class ImportController : Controller
    {
        
        private readonly IIgdbApiService _igdbService;

        
        public ImportController(IIgdbApiService igdbService)
        {
            _igdbService = igdbService;
        }

        public async Task<IActionResult> Index(string? searchQuery)
        {
            
            ViewData["CurrentSearch"] = searchQuery;

            var results = new List<IgdbSearchResultViewModel>();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                results = await _igdbService.SearchGamesAsync(searchQuery);
            }

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> ImportGame(IgdbSearchResultViewModel gameToImport)
        {
            
            var success = await _igdbService.ImportGameAsync(gameToImport);

            if (success)
            {
                return RedirectToAction("Index", "Library");
            }

            return RedirectToAction("Index");
        }
    }
}
