using LowPolyBacklogWeb.Models;

namespace LowPolyBacklogWeb.Services
{
    public interface IBacklogApiService
    {
        Task<IEnumerable<BacklogItemViewModel>> GetAllBacklogsAsync();
        Task<BacklogItemViewModel?> GetBacklogByIdAsync(int id);
        Task<BacklogItemViewModel?> CreateBacklogAsync(BacklogItemViewModel entry);
        Task<bool> UpdateBacklogAsync(int id, BacklogItemViewModel entry);
        Task<bool> DeleteBacklogAsync(int id);
    }
}
