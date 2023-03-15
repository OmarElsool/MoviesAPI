using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Dtos
{
    public class MoviesDetailsDto : MovieParentDto
    {
        public int Id { get; set; }
        public byte[] Poster { get; set; }
        public string GenreName { get; set; }
    }
}
