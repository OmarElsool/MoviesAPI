using System.ComponentModel.DataAnnotations;

namespace Movies.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = "";
    }
}
