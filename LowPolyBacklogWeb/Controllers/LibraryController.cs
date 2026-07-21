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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var gameDetails = await _gameService.GetGameByIdAsync(id);

            if (gameDetails == null)
            {
                return NotFound(); 
            }

            ViewBag.AvailableGenres = await _gameService.GetAllGenresAsync();
            var editModel = new GameUpdateViewModel
            {
                Id = gameDetails.Id,
                Title = gameDetails.Title,
                Synopsis = gameDetails.Synopsis,
                ReleaseYear = gameDetails.ReleaseYear,
                Developer = gameDetails.Developer,
                CoverImageUrl = gameDetails.CoverImageUrl,

            };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GameUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccess = await _gameService.UpdateGameAsync(id, model);

            if (isSuccess)
            {
                return RedirectToAction("Details", new { id = model.Id });
            }

            ModelState.AddModelError(string.Empty, "Hubo un error al intentar actualizar el juego en la API.");
            return View(model);
        }


    }
}
