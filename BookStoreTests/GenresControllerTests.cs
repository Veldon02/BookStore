using BookStore.Controllers;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookStoreTests
{
    public class GenresControllerTests
    {
        private readonly Mock<IGenreRepository> _mockRepository;
        private readonly GenresController _controller;

        public GenresControllerTests()
        {
            _mockRepository = new Mock<IGenreRepository>();
            _controller = new GenresController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetGenres_ReturnsOkResult()
        {
            // Arrange
            var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Horror" },
            new Genre { Id = 2, Name = "Science Fiction" },
        };
            _mockRepository.Setup(repo => repo.GetGenresAsync()).ReturnsAsync(genres);

            // Act
            var result = await _controller.GetGenres();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetGenre_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var genre = new Genre { Id = 1, Name = "Horror" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(genre.Id)).ReturnsAsync(genre);

            // Act
            var result = await _controller.GetGenre(genre.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetGenre_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = -1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Genre)null);

            // Act
            var result = await _controller.GetGenre(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostGenre_WithValidModel_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var genre = new Genre { Id = 1, Name = "Horror" };
            _mockRepository.Setup(repo => repo.AddAsync(genre));

            // Act
            var result = await _controller.PostGenre(genre);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PutGenre_WithValidIdAndModel_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var genre = new Genre { Id = id, Name = "Horror" };
            _mockRepository.Setup(repo => repo.Update(genre));

            // Act
            var result = await _controller.PutGenre(id, genre);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutGenre_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidId = -1;
            var genre = new Genre { Id = 1, Name = "Horror" };

            // Act
            var result = await _controller.PutGenre(invalidId, genre);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteGenre_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var genre = new Genre { Id = id, Name = "Horror" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(genre);

            // Act
            var result = await _controller.DeleteGenre(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteGenre_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = -1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Genre)null);

            // Act
            var result = await _controller.DeleteGenre(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
