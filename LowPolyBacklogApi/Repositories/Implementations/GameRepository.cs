using LowPolyBacklogApi.Data;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LowPolyBacklogApi.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync(string? title, string? genre, int? year)
        {
            IQueryable<Game> query = _context.Games
                .Include(g => g.Genres);

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(g => g.Title.ToLower().Contains(title.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(g => g.Genres.Any(gen => gen.Name.ToLower() == genre.ToLower()));
            }

            if (year.HasValue)
            {
                query = query.Where(g => g.ReleaseYear == year);
            }

            return await query.ToListAsync();

        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            var game = await _context.Games
                .Include(g => g.Genres)
                .FirstOrDefaultAsync(g => g.Id == id);

            return game;

        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);

            await _context.SaveChangesAsync();
        }
    }
}
