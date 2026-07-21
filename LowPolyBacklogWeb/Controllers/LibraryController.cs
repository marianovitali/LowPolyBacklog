using LowPolyBacklogWeb.Models;
using LowPolyBacklogWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LowPolyBacklogWeb.Controllers
{
    public class LibraryController : Controller
    {

        private readonly IGameApiService _gameService;

        public LibraryController(IGameApiService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index([FromQuery] GameFilterViewModel filters)
        {
            var gamesResult = await _gameService.GetGamesAsync(filters);

            var viewModel = new LibraryViewModel
            {
                Games = gamesResult,
                Filters = filters 
            };

            return View(viewModel);
        }

        [HttpGet("Library/Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {

            var gameDetails = await _gameService.GetGameByIdAsync(id);

            if (gameDetails == null)
            {
                // Cambiar a RedirectToAction con 404 NOT FOUND PSX MEMORY CARD.
                return NotFound();
            }

            return View(gameDetails);
        }
    }
}
