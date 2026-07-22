using LowPolyBacklogWeb.Models;

namespace LowPolyBacklogWeb.Services
{
    public interface IGameApiService
    {
        Task<PagedResponse<GameViewModel>> GetGamesAsync(GameFilterViewModel filters);
        Task<GameDetailsViewModel?> GetGameByIdAsync(int id);
        Task<bool> UpdateGameAsync(int id, GameUpdateViewModel updatedGame);

        Task<IEnumerable<GenreViewModel>> GetAllGenresAsync();
        Task<bool> DeleteGameAsync(int id);
    }
}
