using Hw13.Configuration;
using Hw13.Contracts;
using Hw13.Entities;
using Hw13.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public List<Book> GetListOfAllBooks()
        {
            return _context.Books.AsNoTracking().ToList();
        }
        public List<Book> GetListOfBookEndTime()
        {
            var books = _context.Books.AsNoTracking().ToList();
            var filteredBooks = books.Where(b => b.Status == StatusBookEnum.Borrowed &&
                                                  b.EndBorrowedDate.HasValue)
                                                  .ToList();

            return filteredBooks;
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == id);
        }

        public List<Book> GetUserBooks(int userId)
        {
            return _context.Books
                .AsNoTracking()
                .Where(b => b.UserId == userId && b.Status == StatusBookEnum.Borrowed)
                .ToList();
        }
        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        public void AddEndTimeBarrow(int bookId, int newEndTimeBarrow)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId && b.Status == StatusBookEnum.Borrowed);
            if (book != null)
            {
                book.EndBorrowedDate = book.EndBorrowedDate.Value.AddDays(newEndTimeBarrow);
                _context.Books.Update(book);
                _context.SaveChanges();
            }
        }


    }
}
