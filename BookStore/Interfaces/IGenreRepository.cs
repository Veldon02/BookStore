using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(int id);
        Task<List<Genre>> GetGenresAsync();
        Task AddAsync(Genre genre);
        void Update(Genre genre);
        void Remove(Genre genre);
    }

}
