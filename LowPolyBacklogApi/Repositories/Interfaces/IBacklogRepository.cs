using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Repositories.Interfaces
{
    public interface IBacklogRepository
    {

        Task<IEnumerable<BacklogEntry>> GetAllAsync();
        Task<BacklogEntry?> GetByIdAsync(Guid id);
        Task AddAsync(BacklogEntry entry);
        Task UpdateAsync(BacklogEntry entry);
        Task DeleteAsync(BacklogEntry entry);

    }
}
