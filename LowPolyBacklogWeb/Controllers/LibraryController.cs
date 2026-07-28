using LowPolyBacklogWeb.Filters;
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
                GenreIds = gameDetails.Genres.Select(g => g.Id).ToList()
            };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAdmin]
        public async Task<IActionResult> Edit(int id, GameUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.AvailableGenres = await _gameService.GetAllGenresAsync();
                return View(model);
            }

            var isSuccess = await _gameService.UpdateGameAsync(id, model);

            if (isSuccess)
            {
                return RedirectToAction("Details", new { id = model.Id });
            }

            ViewBag.AvailableGenres = await _gameService.GetAllGenresAsync();
            ModelState.AddModelError(string.Empty, "Hubo un error al intentar actualizar el juego en la API.");
            return View(model);
        }

        [HttpPost]
        [RequireAdmin]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _gameService.DeleteGameAsync(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hubo un error al intentar eliminar el juego.";
            return RedirectToAction("Details", new { id = id });
        }


    }
}
