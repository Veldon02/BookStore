using BookStore.Models;
using BookStore.Persistence.Repostories;
using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;
using BookStoreTests.Comparers;

namespace BookStoreTests
{
    public class AuthorRepositoryTests
    {
        private readonly BooksDbContext _context;
        private readonly AuthorRepository _repository;
        private readonly Random _random = new();

        public AuthorRepositoryTests()
        {
            //Set up options for the in-memory database
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new BooksDbContext(options);
            _repository = new AuthorRepository(_context);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectAuthor()
        {
            //Arrange
            var expectedAuthor = new Author { Id = 1, Name = "Test Author 1", Books = new List<Book> { new Book { Id = 1, Title = "Test Book 1", Price = 10.99m, QuantityAvailable = 5, Genre = new Genre { Id = 1, Name = "Test Genre 1" } } } };
            _context.Authors.Add(expectedAuthor);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByIdAsync(expectedAuthor.Id);

            //Assert
            Assert.Equal(expectedAuthor, result, new AuthorEqualityComparer());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullWhenAuthorNotFound()
        {
            //Arrange

            //Act
            var result = await _repository.GetByIdAsync(_random.Next(1, 100));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAuthorsAsync_ReturnsCorrectListOfAuthors()
        {
            //Arrange
            var expectedAuthors = new List<Author>
        {
            new Author { Id = 1, Name = "Test Author 1", Books = new List<Book> { new Book { Id = 1, Title = "Test Book 1", Price = 10.99m, QuantityAvailable = 5, Genre = new Genre { Id = 1, Name = "Test Genre 1" } } } },
            new Author { Id = 2, Name = "Test Author 2", Books = new List<Book> { new Book { Id = 2, Title = "Test Book 2", Price = 20.99m, QuantityAvailable = 3, Genre = new Genre { Id = 2, Name = "Test Genre 2" } } } },
            new Author { Id = 3, Name = "Test Author 3", Books = new List<Book> { new Book { Id = 3, Title = "Test Book 3", Price = 15.99m, QuantityAvailable = 7, Genre = new Genre { Id = 3, Name = "Test Genre 3" } } } },
            new Author { Id = 4, Name = "Empty Author", Books = new List<Book>() }
        };
            _context.Authors.AddRange(expectedAuthors);
            await _context.SaveChangesAsync();


            //Act
            var result = await _repository.GetAuthorsAsync();

            //Assert
            Assert.Equal(expectedAuthors, result, new AuthorEqualityComparer());
        }

        [Fact]
        public async Task AddAsync_AddsAuthorToDatabase()
        {
            //Arrange
            var authorToAdd = new Author { Name = "Test Author" };

            //Act
            await _repository.AddAsync(authorToAdd);

            //Assert
            Assert.Contains(authorToAdd, _context.Authors.ToList(), new AuthorEqualityComparer());
        }

        [Fact]
        public void Update_UpdatesAuthorInDatabase()
        {
            //Arrange
            var authorToUpdate = new Author { Id = 1, Name = "Test Author" };
            _context.Authors.Add(authorToUpdate);
            _context.SaveChanges();

            //Act
            authorToUpdate.Name = "Updated Test Author";
            _repository.Update(authorToUpdate);

            //Assert
            Assert.Contains(authorToUpdate, _context.Authors.ToList(), new AuthorEqualityComparer());
        }

        [Fact]
        public void Remove_RemovesAuthorFromDatabase()
        {
            //Arrange
            var authorToRemove = new Author { Id = 1, Name = "Test Author" };
            _context.Authors.Add(authorToRemove);
            _context.SaveChanges();

            //Act
            _repository.Remove(authorToRemove);

            //Assert
            Assert.DoesNotContain(authorToRemove, _context.Authors.ToList(), new AuthorEqualityComparer());
        }

        

        private class BookEqualityComparer : IEqualityComparer<Book>
        {
            public bool Equals(Book x, Book y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return x.Id == y.Id
                    && x.Title == y.Title
                    && x.Price == y.Price
                    && x.QuantityAvailable == y.QuantityAvailable
                    && new GenreEqualityComparer().Equals(x.Genre, y.Genre);
            }

            public int GetHashCode(Book obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
