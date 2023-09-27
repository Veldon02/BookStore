using BookStore.Models;
using BookStore.Persistence.Repostories;
using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using BookStoreTests.Comparers;

namespace BookStoreTests
{
    public class BookRepositoryTests
    {
        private readonly DbContextOptions<BooksDbContext> _options;
        private readonly BooksDbContext _context;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDb")
                .Options;

            _context = new BooksDbContext(_options);
            _repository = new BookRepository(_context);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectBook()
        {
            //Arrange
            var author = new Author { Id = 1, Name = "Test Author" };
            var genre = new Genre { Id = 1, Name = "Test Genre" };
            var expectedBook = new Book { Id = 1, Title = "Test Book", Author = author, Genre = genre, Price = 10.99m, QuantityAvailable = 5 };
            _context.Authors.Add(author);
            _context.Genres.Add(genre);
            _context.Books.Add(expectedBook);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByIdAsync(1);

            //Assert
            Assert.Equal(expectedBook, result, new BookEqualityComparer());
        }

        [Fact]
        public async Task GetBooksAsync_ReturnsCorrectBooks()
        {
            //Arrange
            var author1 = new Author { Id = 1, Name = "Test Author 1" };
            var author2 = new Author { Id = 2, Name = "Test Author 2" };
            var genre1 = new Genre { Id = 1, Name = "Test Genre 1" };
            var genre2 = new Genre { Id = 2, Name = "Test Genre 2" };
            var expectedBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book 1", Author = author1, Genre = genre1, Price = 10.99m, QuantityAvailable = 5 },
            new Book { Id = 2, Title = "Test Book 2", Author = author2, Genre = genre2, Price = 20.99m, QuantityAvailable = 3 },
            new Book { Id = 3, Title = "Test Book 3", Author = author1, Genre = genre1, Price = 15.99m, QuantityAvailable = 7 }
        };
            _context.Authors.AddRange(author1, author2);
            _context.Genres.AddRange(genre1, genre2);
            _context.Books.AddRange(expectedBooks);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetBooksAsync();

            //Assert
            Assert.Equal(expectedBooks, result, new BookListEqualityComparer());
        }

        [Fact]
        public async Task AddAsync_AddsNewBook()
        {
            //Arrange
            var author = new Author { Id = 1, Name = "Test Author" };
            var genre = new Genre { Id = 1, Name = "Test Genre" };
            var newBook = new Book { Title = "New Test Book", Author = author, Genre = genre, Price = 12.99m, QuantityAvailable = 6 };
            _context.Authors.Add(author);
            _context.Genres.Add(genre);

            //Act
            await _repository.AddAsync(newBook);
            var result = await _repository.GetByIdAsync(newBook.Id);

            //Assert
            Assert.Equal(newBook, result, new BookEqualityComparer());
        }

        [Fact]
        public async Task Update_UpdatesExistingBook()
        {
            //Arrange
            var author1 = new Author { Id = 1, Name = "Test Author 1" };
            var author2 = new Author { Id = 2, Name = "Test Author 2" };
            var genre1 = new Genre { Id = 1, Name = "Test Genre 1" };
            var genre2 = new Genre { Id = 2, Name = "Test Genre 2" };
            var existingBook = new Book { Id = 1, Title = "Test Book 1", Author = author1, Genre = genre1, Price = 10.99m, QuantityAvailable = 5 };
            _context.Authors.AddRange(author1, author2);
            _context.Genres.AddRange(genre1, genre2);
            _context.Books.Add(existingBook);
            await _context.SaveChangesAsync();

            var bookToUpdate = await _context.Books.FindAsync(existingBook.Id);
            bookToUpdate.Title = "Updated Test Book";
            bookToUpdate.Author = author2;
            bookToUpdate.Genre = genre2;
            bookToUpdate.Price = 20.99m;
            bookToUpdate.QuantityAvailable = 3;
            //Act
            _repository.Update(bookToUpdate);
            var result = await _repository.GetByIdAsync(1);

            //Assert
            Assert.Equal(bookToUpdate, result, new BookEqualityComparer());
        }

        [Fact]
        public async Task Remove_RemovesExistingBook()
        {
            //Arrange
            var author = new Author { Id = 1, Name = "Test Author" };
            var genre = new Genre { Id = 1, Name = "Test Genre" };
            var existingBook = new Book { Id = 1, Title = "Test Book 1", Author = author, Genre = genre, Price = 10.99m, QuantityAvailable = 5 };
            _context.Authors.Add(author);
            _context.Genres.Add(genre);
            _context.Books.Add(existingBook);
            await _context.SaveChangesAsync();

            //Act
            _repository.Remove(existingBook);
            var result = await _repository.GetByIdAsync(1);

            //Assert
            Assert.Null(result);
        }
    }
}
