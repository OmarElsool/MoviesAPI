using System.ComponentModel.DataAnnotations;

namespace Movies.Dtos
{
    public class MovieParentDto
    {
        [Required]
        public string Title { get; set; } = "";
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500), Required]
        public string StoryLine { get; set; } = "";
        public byte GenreId { get; set; }
    }
}
