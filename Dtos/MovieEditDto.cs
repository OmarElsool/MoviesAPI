namespace Movies.Dtos
{
    public class MovieEditDto : MovieParentDto
    {
        public IFormFile? Poster { get; set; }
    }
}
