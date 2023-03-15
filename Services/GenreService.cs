using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext db;
        public GenreService(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<Genre> Add(Genre genre)
        {
            await db.Genres.AddAsync(genre);
            db.SaveChanges();
            return genre;
        }

        public Task<bool> AllowedGenre(byte id)
        {
            return db.Genres.AnyAsync(d => d.Id == id);
        }

        public Genre Delete(Genre genre)
        {
            db.Remove(genre);
            db.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await db.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await db.Genres.FindAsync(id);
        }

        public Genre Update(Genre genre)
        {
            db.Update(genre);
            db.SaveChanges();
            return genre;
        }
    }
}
