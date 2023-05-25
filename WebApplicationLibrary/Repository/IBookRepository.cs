
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplicationLibrary.Models;

namespace WebApplicationLibrary.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Added(Book book);  
        void Update(Book book); 
        void Delete(int id);
        IEnumerable<Book> GetAllSorted(string orderBy);
        IEnumerable<Book> GetRecommendedBooks(string genre);
    }

    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetRecommendedBooks(string genreName)
        {
            return _context.Books
                .Where(b => b.Genre.Name == genreName)
                .Where(b => b.ReviewsNumber > 10)
                .OrderByDescending(b => b.Rating)
                .Take(10)
                .ToList();
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _context.Books
                            .Include(book => book.Author)
                            .Include(book => book.Genre);
            return books;
        }



        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Added(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetAllSorted(string orderBy)
        {
            switch (orderBy.ToLower())
            {
                case "author":
                    return _context.Books.OrderBy(b => b.Author);
                default:
                    return _context.Books.OrderBy(b => b.Title);
            }
        }
    }

}