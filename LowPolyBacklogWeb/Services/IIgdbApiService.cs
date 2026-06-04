using LowPolyBacklogWeb.Models;

namespace LowPolyBacklogWeb.Services
{
    public interface IIgdbApiService
    {
        Task<List<IgdbSearchResultViewModel>> SearchGamesAsync(string query);

    }
}
