using LowPolyBacklogApi.DTOs.Backlog;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogWeb.Models;
using LowPolyBacklogWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogWeb.Controllers
{
    public class BacklogController : Controller
    {
        private readonly IBacklogApiService _backlogApiService;

        public BacklogController(IBacklogApiService backlogApiService)
        {
            _backlogApiService = backlogApiService;
        }

        public async Task<IActionResult> Index()
        {

            var allEntries = await _backlogApiService.GetAllBacklogsAsync();

            var board = new BacklogBoardViewModel
            {
                Pendientes = allEntries.Where(b => b.Status == PlayStatus.Pending).ToList(),
                Jugando = allEntries.Where(b => b.Status == PlayStatus.Playing).ToList(),
                Pausados = allEntries.Where(b => b.Status == PlayStatus.Paused).ToList(),
                Completados = allEntries.Where(b => b.Status == PlayStatus.Completed).ToList()
            };

            return View(board);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBacklog(int gameId)
        {
            var newEntry = new BacklogItemViewModel
            {
                GameId = gameId,
                Status = PlayStatus.Pending,
                HoursPlayed = 0
            };

            var result = await _backlogApiService.CreateBacklogAsync(newEntry);

            if (result != null)
            {
                return RedirectToAction("Index", "Backlog");
            }

            return RedirectToAction("Details", "Library", new { id = gameId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var backlogItem = await _backlogApiService.GetBacklogByIdAsync(id);
            if (backlogItem == null)
            {
                return NotFound();
            }

            return View(backlogItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BacklogItemViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var success = await _backlogApiService.UpdateBacklogAsync(id, model);

            if (success)
            {
                return RedirectToAction("Details", "Library", new { id = model.GameId });
            }
            ModelState.AddModelError("", "Hubo un error al actualizar el backlog.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateKanbanStatus([FromBody] KanbanUpdateRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var currentBacklog = await _backlogApiService.GetBacklogByIdAsync(request.Id);
                if (currentBacklog == null) return NotFound();

                // Convertimos el string ("Pending", "Playing", etc) al Enum real de tu API.
                // El true al final es para ignorar mayúsculas/minúsculas y que no falle por eso.
                currentBacklog.Status = Enum.Parse<LowPolyBacklogApi.Entities.PlayStatus>(request.Status, true);

                await _backlogApiService.UpdateBacklogAsync(request.Id, currentBacklog);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al actualizar el estado." });
            }
        }


    }
}
