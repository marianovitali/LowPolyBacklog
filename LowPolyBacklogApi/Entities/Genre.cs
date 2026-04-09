namespace LowPolyBacklogApi.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
