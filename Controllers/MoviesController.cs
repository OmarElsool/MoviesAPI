using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Dtos;
using Movies.Models;
using Movies.Services;

namespace Movies.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        //, ".jpeg"
        private List<string> allowedExt = new List<string>() { ".jpg", ".png" };
        private long allowedPosterSize = 1048576; //1mb
        public MoviesController(AppDbContext _db, IMovieService _movieService, IGenreService _genreService, IMapper mapper)
        {
            movieService = _movieService;
            genreService = _genreService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await movieService.GetAll();
            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movies);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var movie = await db.Movies.Include(m => m.Genre)
            //.Select(m => new MoviesDetailsDto
            //{
            //    Id = m.Id,
            //    Rate = m.Rate,
            //    StoryLine = m.StoryLine,
            //    GenreId = m.GenreId,
            //    Title = m.Title,
            //    Year = m.Year,
            //    Poster = m.Poster,
            //    GenreName = m.Genre.Name
            //}).FirstOrDefaultAsync(m => m.Id == id);
            var movie = await movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<MoviesDetailsDto>(movie);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpGet("GetByGenre/{genreId}")]
        public async Task<IActionResult> GetByGenre(byte genreId)
        {
            //var moviesByGenre = await db.Movies.Where(m => m.GenreId == genreId).Include(m => m.Genre)
            //.Select(m => new MoviesDetailsDto
            //{
            //    Id = m.Id,
            //    Rate = m.Rate,
            //    StoryLine = m.StoryLine,
            //    GenreId = m.GenreId,
            //    Title = m.Title,
            //    Year = m.Year,
            //    Poster = m.Poster,
            //    GenreName = m.Genre.Name
            //})
            //.OrderByDescending(m => m.Rate)
            //.ToListAsync();
            var moviesByGenre = await movieService.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(moviesByGenre);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieDto dto)
        {
            if (!allowedExt.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Not Allowed Extension");
            }
            if (dto.Poster.Length > allowedPosterSize)
            {
                return BadRequest("Max Allowed Size Is 1 MB");
            }
            var AllowedGenre = await genreService.AllowedGenre(dto.GenreId);
            if (!AllowedGenre)
            {
                return BadRequest("InValid Genre");
            }

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();
            movieService.Add(movie);
            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] MovieEditDto dto)
        {
            var movie = await movieService.GetById(id);
            if (movie == null)
            {
                return NotFound($"Movie Id: {id} Not Found");
            }
            var AllowedGenre = await genreService.AllowedGenre(dto.GenreId);
            if (!AllowedGenre)
            {
                return BadRequest("InValid Genre");
            }
            movie.Title = dto.Title;
            movie.StoryLine = dto.StoryLine;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.GenreId = dto.GenreId;
            if (dto.Poster != null)
            {
                if (!allowedExt.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("Not Allowed Extension");
                }
                if (dto.Poster.Length > allowedPosterSize)
                {
                    return BadRequest("Max Allowed Size Is 1 MB");
                }
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }
            movieService.Update(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            movieService.Delete(movie);
            return Ok(movie);
        }
    }
}
