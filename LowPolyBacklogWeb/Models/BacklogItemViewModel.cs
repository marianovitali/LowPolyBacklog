using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogWeb.Models
{
    public class BacklogItemViewModel
    {
        public int Id { get; set; }
        public PlayStatus Status { get; set; }
        public int? Rating { get; set; }
        public int HoursPlayed { get; set; }
        public string? ReviewNotes { get; set; }
        public int GameId { get; set; }
        public GameViewModel Game { get; set; } // Acá anidamos los datos del juego (título, portada)
    }
}
}
