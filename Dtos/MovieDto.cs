using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Dtos
{
    public class MovieDto : MovieParentDto
    {
        public IFormFile Poster { get; set; }

    }
}
