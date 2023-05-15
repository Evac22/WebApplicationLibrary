using WebApplicationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplicationLibrary.Repositories
{
    public interface IBorrowRepository
    {
        IEnumerable<Borrow> GetAll();
        Borrow GetById(int id);
        void Add(Borrow borrow);
        void Update(Borrow borrow);
        void Delete(int id);
        IEnumerable<Borrow> GetByBookId(int bookId);
        IEnumerable<Borrow> GetByUserId(int userId);
        IEnumerable<Borrow> GetOverdue(DateTime currentDate);
    }

    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryContext _context;

        public BorrowRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Borrow> GetAll()
        {
            return _context.Borrows
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.User)
                .ToList();
        }

        public Borrow GetById(int id)
        {
            return _context.Borrows
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Borrow borrow)
        {
            _context.Borrows.Add(borrow);
            _context.SaveChanges();
        }

        public void Update(Borrow borrow)
        {
            _context.Entry(borrow).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var borrow = _context.Borrows.FirstOrDefault(b => b.Id == id);
            if(borrow!= null)
            {
                _context.Borrows.Remove(borrow);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Borrow> GetByBookId(int bookId)
        {
            return _context.Borrows
                  .Include(b => b.Book)
                      .ThenInclude(b => b.Author)
                  .Include(b => b.User)
                  .Where(b => b.BookId == bookId)
                  .ToList();
        }

        public IEnumerable<Borrow> GetByUserId(int userId)
        {
            return _context.Borrows
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToList();
        }

        public IEnumerable<Borrow> GetOverdue(DateTime currentDate)
        {
            return _context.Borrows
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.User)
                .Where(b => b.DueDate < currentDate && !b.ReturnedDate.HasValue)
                .ToList();
        }
    }
}