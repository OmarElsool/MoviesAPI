using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Dtos;
using Movies.Models;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext db;

        public MovieService(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await db.Movies.AddAsync(movie);
            db.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            db.Remove(movie);
            db.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await db.Movies.Include(m => m.Genre)
                                    .Where(m => m.GenreId == genreId || genreId == 0)
                                    .OrderByDescending(m => m.Rate)
                                    .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await db.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
            db.Update(movie);
            db.SaveChanges();
            return movie;
        }
    }
}
