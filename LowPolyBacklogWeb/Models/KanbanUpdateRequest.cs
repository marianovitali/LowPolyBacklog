namespace LowPolyBacklogWeb.Models
{
    public class KanbanUpdateRequest
    {
        public int Id { get; set; }
        //public LowPolyBacklogApi.Entities.PlayStatus Status { get; set; }

        public string Status { get; set; }
    }
}
