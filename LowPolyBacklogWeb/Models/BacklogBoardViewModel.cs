namespace LowPolyBacklogWeb.Models
{
    public class BacklogBoardViewModel
    {
        public List<BacklogItemViewModel> Pendientes { get; set; } = new();
        public List<BacklogItemViewModel> Jugando { get; set; } = new();
        public List<BacklogItemViewModel> Pausados { get; set; } = new();
        public List<BacklogItemViewModel> Completados { get; set; } = new();
    }
}
