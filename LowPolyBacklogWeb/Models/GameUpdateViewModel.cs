using System.ComponentModel.DataAnnotations;

namespace LowPolyBacklogWeb.Models
{
    public class GameUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Game title is required.")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Synopsis cannot exceed 500 characters.")]
        public string? Synopsis { get; set; }

        [Range(1994, 2006, ErrorMessage = "Valid PS1 release years are between 1994 and 2006.")]
        public int ReleaseYear { get; set; }

        [MaxLength(100)]
        public string? Developer { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? CoverImageUrl { get; set; }

        [Range(1, 10, ErrorMessage = "Disc count must be between 1 and 10.")]
        public int DiscCount { get; set; }

        [MinLength(1, ErrorMessage = "You must select at least one genre.")]
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
