using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.Repostories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;

        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Genre).SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public async Task<List<Book>> SearchByTitleAsync(string title)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Genre).Where(b => b.Title.ToLower().Contains(title.ToLower())).ToListAsync();
        }

        public async Task<List<Book>> SearchByAuthorAsync(string author)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Genre).Where(b => b.Author.Name.ToLower().Contains(author.ToLower())).ToListAsync();
        }

        public async Task<List<Book>> SearchByGenreAsync(string genre)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Genre).Where(b => b.Genre.Name.ToLower().Contains(genre.ToLower())).ToListAsync();
        }
    }
}
