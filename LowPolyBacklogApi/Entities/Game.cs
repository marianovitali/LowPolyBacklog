namespace LowPolyBacklogApi.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public string? Synopsis { get; set; }
        public int ReleaseYear { get; set; }

        public string? Developer { get; set; }

        // Acá va a ir la URL que nos devuelva Cloudinary en el futuro
        public string? CoverImageUrl { get; set; }

        // Por defecto, los juegos de PS1 traen 1 disco, pero lo podemos pisar si es un Final Fantasy
        public int DiscCount { get; set; } = 1;

        // Relación de muchos a muchos (Un juego tiene muchos géneros)
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
