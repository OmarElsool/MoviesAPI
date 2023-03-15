using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500), Required]
        public string StoryLine { get; set; } = "";
        public byte[] Poster { get; set; }

        [ForeignKey("Genre")]
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
