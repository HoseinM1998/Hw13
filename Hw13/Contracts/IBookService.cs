using Hw13.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Contracts
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        public Book GetBookById(int id);
        public List<Book> GetListOfBookEndTime();
        List<Book> GetUserBooks(int userId);
        bool BorrowBook(int userId, int bookId);
        bool ReturnBook(int userId, int bookId);
        public void AddEndTimeBarrow(int bookId, int newEndTimeBarrow);
    }
}
