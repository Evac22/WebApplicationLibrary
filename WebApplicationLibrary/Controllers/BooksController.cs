using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Repositories;
using WebApplicationLibrary.DTO;

namespace WebApplicationLibrary.Controllers
{
    //атрибут для определения URL-адреса, по которому будет доступен контроллер
    [Route("api/[controller]")]
    //контроллер является контроллером API
    [ApiController]
    public class BooksController : ControllerBase
    {
        //это контекст базы данных, который используется для получения данных из базы данных
        private readonly LibraryContext _context;

        //это интерфейс репозитория для работы с данными книг, который используется для получения и сохранения данных
        private readonly IBookRepository _bookRepository;
        public BooksController(LibraryContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        // метод для получения всех книг в бд
        // Get: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        // метод для получения книги по id
        // Get: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
        // выполнение условия задачи: Получить все книги. Сортировка по заданному значению (названию или автору)
        //GET https://{{baseUrl}}/api/books?order=author
        [HttpGet("GetAllSorted")]
        public IActionResult GetAllSorted(string order = "title")
        {
            try
            {
                var books = _bookRepository.GetAllSorted(order);
                var bookDtos = books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating,
                    ReviewsNumber = b.ReviewsNumber
                });
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // метод для обновления информации о книге
        // PUT: api/Books/5
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("recommended")]
        public IActionResult GetRecommendedBooks(string genre)
        {
            var recommendedBooks = _bookRepository.GetRecommendedBooks(genre);

            var bookDtos = recommendedBooks
                .OrderByDescending(b => b.Rating)
                .Take(10)
                .Where(b => b.ReviewsNumber > 10)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating,
                    ReviewsNumber = b.ReviewsNumber
                });

            return Ok(bookDtos);
        }


        // метод для добавления новой книги в бд
        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new {id = book.Id }, book);
        }
        // метод для удаления книги из бд по id
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        // метод для проверки наличия книги в бд по id
        private bool BookExists(int id) 
        { 
            return _context.Books.Any(e => e.Id == id);
        }
    }
}