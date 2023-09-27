using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _bookRepository.GetBooksAsync());
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            await _bookRepository.AddAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _bookRepository.Update(book);

            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Remove(book);

            return NoContent();
        }

        // GET: api/books/search/title?title=keyword
        [HttpGet("search/title")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByTitle(string title)
        {
            var books = await _bookRepository.SearchByTitleAsync(title);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // GET: api/books/search/author?author=keyword
        [HttpGet("search/author")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByAuthor(string author)
        {
            var books = await _bookRepository.SearchByAuthorAsync(author);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // GET: api/books/search/genre?genre=keyword
        [HttpGet("search/genre")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByGenre(string genre)
        {
            var books = await _bookRepository.SearchByGenreAsync(genre);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }
    }
}
