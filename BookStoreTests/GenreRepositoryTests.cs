using BookStore.Models;
using BookStore.Persistence.Repostories;
using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;
using BookStoreTests.Comparers;

namespace BookStoreTests
{
    public class GenreRepositoryTests
    {
        private readonly BooksDbContext _context;
        private readonly Random _random = new();
        private readonly GenreRepository _repository;

        public GenreRepositoryTests()
        {
            // Set up options for the in-memory database
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Initialize DbContext and repository
            _context = new BooksDbContext(options);
            _repository = new GenreRepository(_context);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectGenre()
        {
            // Arrange
            var expectedGenre = new Genre { Id = 1, Name = "Test Genre 1", Books = new List<Book> { new Book { Id = 1, Title = "Test Book 1", Price = 10.99m, QuantityAvailable = 5, Genre = new Genre { Id = 1, Name = "Test Genre 1" } } } };
            _context.Genres.Add(expectedGenre);
            await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(expectedGenre.Id);

        // Assert
        Assert.Equal(expectedGenre, result, new GenreEqualityComparer());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullWhenGenreNotFound()
        {
            // Act
            var result = await _repository.GetByIdAsync(_random.Next(1, 100));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetGenresAsync_ReturnsCorrectListOfGenres()
        {
            // Arrange
            var expectedGenres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Test Genre 1", Books = new List<Book> { new Book { Id = 1, Title = "Test Book 1", Price = 10.99m, QuantityAvailable = 5, Genre = new Genre { Id = 1, Name = "Test Genre 1" } } } },
                new Genre { Id = 2, Name = "Test Genre 2", Books = new List<Book> { new Book { Id = 2, Title = "Test Book 2", Price = 20.99m, QuantityAvailable = 3, Genre = new Genre { Id = 2, Name = "Test Genre 2" } } } },
                new Genre { Id = 3, Name = "Test Genre 3", Books = new List<Book> { new Book { Id = 3, Title = "Test Book 3", Price = 15.99m, QuantityAvailable = 7, Genre = new Genre { Id = 3, Name = "Test Genre 3" } } } },
                new Genre { Id = 4, Name = "Empty Genre", Books = new List<Book>() }
            };
            _context.Genres.AddRange(expectedGenres);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetGenresAsync();

            // Assert
            Assert.Equal(expectedGenres, result, new GenreEqualityComparer());
        }

        [Fact]
        public async Task AddAsync_AddsGenreToDatabase()
        {
            // Arrange
            var genreToAdd = new Genre { Name = "Test Genre" };

            // Act
            await _repository.AddAsync(genreToAdd);

            // Assert
            Assert.Contains(genreToAdd, _context.Genres.ToList(), new GenreEqualityComparer());
        }

        [Fact]
        public void Update_UpdatesGenreInDatabase()
        {
            // Arrange
            var genreToUpdate = new Genre { Id = 1, Name = "Test Genre" };
            _context.Genres.Add(genreToUpdate);
            _context.SaveChanges();

            // Act
            genreToUpdate.Name = "Updated Test Genre";
            _repository.Update(genreToUpdate);

            // Assert
            Assert.Contains(genreToUpdate, _context.Genres.ToList(), new GenreEqualityComparer());
        }

        [Fact]
        public void Remove_RemovesGenreFromDatabase()
        {
            // Arrange
            var genreToRemove = new Genre { Id = 1, Name = "Test Genre" };
            _context.Genres.Add(genreToRemove);
            _context.SaveChanges();

            // Act
            _repository.Remove(genreToRemove);

            // Assert
            Assert.DoesNotContain(genreToRemove, _context.Genres.ToList(), new GenreEqualityComparer());
        }
    }
}
