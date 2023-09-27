using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAuthorsAsync();
        Task AddAsync(Author author);
        void Update(Author author);
        void Remove(Author author);
    }
}
