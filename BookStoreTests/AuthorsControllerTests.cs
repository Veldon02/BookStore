using BookStore.Controllers;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookStoreTests
{
    public class AuthorsControllerTests
    {
        private readonly Mock<IAuthorRepository> _mockRepository;
        private readonly AuthorsController _controller;

        public AuthorsControllerTests()
        {
            _mockRepository = new Mock<IAuthorRepository>();
            _controller = new AuthorsController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAuthors_ReturnsOkResult()
        {
            // Arrange
            var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Stephen King" },
            new Author { Id = 2, Name = "J.K. Rowling" },
        };
            _mockRepository.Setup(repo => repo.GetAuthorsAsync()).ReturnsAsync(authors);

            // Act
            var result = await _controller.GetAuthors();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAuthor_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Stephen King" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(author.Id)).ReturnsAsync(author);

            // Act
            var result = await _controller.GetAuthor(author.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAuthor_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = -1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Author)null);

            // Act
            var result = await _controller.GetAuthor(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostAuthor_WithValidModel_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Stephen King" };
            _mockRepository.Setup(repo => repo.AddAsync(author));

            // Act
            var result = await _controller.PostAuthor(author);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PutAuthor_WithValidIdAndModel_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var author = new Author { Id = 1, Name = "Stephen King" };
            _mockRepository.Setup(repo => repo.Update(author));

            // Act
            var result = await _controller.PutAuthor(id, author);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutAuthor_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidId = -1;
            var author = new Author { Id = 1, Name = "Stephen King" };

            // Act
            var result = await _controller.PutAuthor(invalidId, author);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAuthor_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var author = new Author { Id = 1, Name = "Stephen King" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(author);

            // Act
            var result = await _controller.DeleteAuthor(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAuthor_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = -1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Author)null);

            // Act
            var result = await _controller.DeleteAuthor(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
