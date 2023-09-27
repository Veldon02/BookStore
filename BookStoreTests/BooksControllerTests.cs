using BookStore.Controllers;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookStoreTests
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _controller = new BooksController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult()
        {
            // Arrange
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 },
            new Book { Id = 2, Title = "Harry Potter and the Philosopher's Stone", Author = new Author { Id = 2, Name = "J.K. Rowling" }, Genre = new Genre { Id = 2, Name = "Fantasy" }, Price = 9.99m, QuantityAvailable = 10 },
        };
            _mockRepository.Setup(repo => repo.GetBooksAsync()).ReturnsAsync(books);

            // Act
            var result = await _controller.GetBooks();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetBook_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(book.Id)).ReturnsAsync(book);

            // Act
            var result = await _controller.GetBook(book.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetBook_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = -1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.GetBook(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostBook_WithValidModel_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 };
            _mockRepository.Setup(repo => repo.AddAsync(book));

            // Act
            var result = await _controller.PostBook(book);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PutBook_WithValidIdAndModel_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var book = new Book { Id = id, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 };
            _mockRepository.Setup(repo => repo.Update(book));

            // Act
            var result = await _controller.PutBook(id, book);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutBook_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidId = -1;
            var book = new Book { Id = 1, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 };

            // Act
            var result = await _controller.PutBook(invalidId, book);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteBook_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var book = new Book { Id = id, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(book);

            // Act
            var result = await _controller.DeleteBook(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = -1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.DeleteBook(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task SearchByTitle_WithValidTitle_ReturnsOkResult()
        {
            // Arrange
            var title = "The Shining";
            var books = new List<Book>
        {
            new Book { Id = 1, Title = title, Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 },
            new Book { Id = 2, Title = "The Stand", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 9.99m, QuantityAvailable = 10 },
        };
            _mockRepository.Setup(repo => repo.SearchByTitleAsync(title)).ReturnsAsync(books);

            // Act
            var result = await _controller.SearchByTitle(title);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task SearchByTitle_WithInvalidTitle_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidTitle = "invalid";
            _mockRepository.Setup(repo => repo.SearchByTitleAsync(invalidTitle)).ReturnsAsync((List<Book>)null);

            // Act
            var result = await _controller.SearchByTitle(invalidTitle);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task SearchByAuthor_WithValidAuthor_ReturnsOkResult()
        {
            // Arrange
            var author = "Stephen King";
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "The Shining", Author = new Author { Id = 1, Name = author }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 10.99m, QuantityAvailable = 5 },
            new Book { Id = 2, Title = "The Stand", Author = new Author { Id = 1, Name = author }, Genre = new Genre { Id = 1, Name = "Horror" }, Price = 9.99m, QuantityAvailable = 10 },
        };
            _mockRepository.Setup(repo => repo.SearchByAuthorAsync(author)).ReturnsAsync(books);

            // Act
            var result = await _controller.SearchByAuthor(author);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task SearchByAuthor_WithInvalidAuthor_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidAuthor = "invalid";
            _mockRepository.Setup(repo => repo.SearchByAuthorAsync(invalidAuthor)).ReturnsAsync((List<Book>)null);

            // Act
            var result = await _controller.SearchByAuthor(invalidAuthor);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task SearchByGenre_WithValidGenre_ReturnsOkResult()
        {
            // Arrange
            var genre = "Horror";
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "The Shining", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = genre }, Price = 10.99m, QuantityAvailable = 5 },
            new Book { Id = 2, Title = "The Stand", Author = new Author { Id = 1, Name = "Stephen King" }, Genre = new Genre { Id = 1, Name = genre }, Price = 9.99m, QuantityAvailable = 10 },
        };
            _mockRepository.Setup(repo => repo.SearchByGenreAsync(genre)).ReturnsAsync(books);

            // Act
            var result = await _controller.SearchByGenre(genre);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task SearchByGenre_WithInvalidGenre_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidGenre = "invalid";
            _mockRepository.Setup(repo => repo.SearchByGenreAsync(invalidGenre)).ReturnsAsync((List<Book>)null);

            // Act
            var result = await _controller.SearchByGenre(invalidGenre);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
