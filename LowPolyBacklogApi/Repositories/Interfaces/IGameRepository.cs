using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync(string? title, string? genre, int? year);
        Task<Game?> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(Game game);

    }
}
