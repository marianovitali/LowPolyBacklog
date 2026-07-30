using LowPolyBacklogShared.DTOs.Game;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GenresController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetAll()
        {
            var genres = await _gameService.GetAllGenresAsync();
            return Ok(genres);
        }
    }
}
