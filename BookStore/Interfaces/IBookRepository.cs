using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetBooksAsync();
        Task AddAsync(Book book);
        void Update(Book book);
        void Remove(Book book);
        Task<List<Book>> SearchByGenreAsync(string genre);
        Task<List<Book>> SearchByAuthorAsync(string author);
        Task<List<Book>> SearchByTitleAsync(string title);
    }
}
