namespace LowPolyBacklogApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
