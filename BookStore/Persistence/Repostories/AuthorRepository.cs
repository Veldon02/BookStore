using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.Repostories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksDbContext _context;

        public AuthorRepository(BooksDbContext context)
        {
            _context = context;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(Author author)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
