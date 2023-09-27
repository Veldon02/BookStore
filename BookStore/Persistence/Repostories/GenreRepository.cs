using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.Repostories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BooksDbContext _context;

        public GenreRepository(BooksDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.Include(g => g.Books).SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _context.Genres.Include(g => g.Books).ToListAsync();
        }

        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public void Update(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}
