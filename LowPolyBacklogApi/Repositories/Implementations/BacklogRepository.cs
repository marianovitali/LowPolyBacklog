using LowPolyBacklogApi.Data;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LowPolyBacklogApi.Repositories.Implementations
{
    public class BacklogRepository : IBacklogRepository
    {
        private readonly ApplicationDbContext _context;

        public BacklogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BacklogEntry>> GetAllAsync()
        {
            return await _context.BacklogEntries
                .Include(b => b.Game)
                .ThenInclude(g => g.Genres)
                .OrderByDescending(b => b.Status)
                .ToListAsync();
        }

        public async Task<BacklogEntry?> GetByIdAsync(int id)
        {
            var backlog = await _context.BacklogEntries
                .Include(b => b.Game)
                .FirstOrDefaultAsync(b => b.Id == id);

            return backlog;
        }

        public async Task AddAsync(BacklogEntry entry)
        {
            await _context.BacklogEntries.AddAsync(entry);

            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(BacklogEntry entry)
        {
            _context.BacklogEntries.Remove(entry);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BacklogEntry entry)
        {
            _context.BacklogEntries.Update(entry);

            await _context.SaveChangesAsync();
        }
    }
}
