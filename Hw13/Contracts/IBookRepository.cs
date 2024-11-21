using Hw13.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Contracts
{
    public interface IBookRepository
    {
        public List<Book> GetListOfAllBooks();
        public Book GetBookById(int id);
        public List<Book> GetListOfBookEndTime();
        List<Book> GetUserBooks(int userId);
        public void UpdateBook(Book book);
    }
}
