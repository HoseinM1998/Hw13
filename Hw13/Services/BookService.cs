using Hw13.Contracts;
using Hw13.Entities;
using Hw13.Enum;
using Hw13.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            try
            {
                return _bookRepository.GetListOfAllBooks();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
        }

        public Book GetBookById(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                throw new Exception("Not Found");
            }
            return book; 
        }


        public List<Book> GetListOfBookEndTime()
        {
            try
            {
                return _bookRepository.GetListOfBookEndTime();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
        

        public List<Book> GetUserBooks(int userId)
        {
            try
            {
                return _bookRepository.GetUserBooks(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
        }


        public bool BorrowBook(int userId, int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book != null && book.Status == StatusBookEnum.NotBorrowed)
            {
                book.UserId = userId;
                book.Status = StatusBookEnum.Borrowed;
                book.BorrowedDate = DateTime.Now;
                book.EndBorrowedDate = book.BorrowedDate.Value.AddDays(30);
                _bookRepository.UpdateBook(book); 
                return true;
            }
            return false;
        }

        public bool ReturnBook(int userId, int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book != null && book.Status == StatusBookEnum.Borrowed && book.UserId == userId)
            {
                book.UserId = null;
                book.Status = StatusBookEnum.NotBorrowed;
                book.BorrowedDate = null;
                book.EndBorrowedDate = null;
                _bookRepository.UpdateBook(book);
                
                return true;
            }
            return false;
        }

        public void AddEndTimeBarrow(int bookId, int newEndTimeBarrow)
        {
            var book = _bookRepository.GetBookById(bookId);
            _bookRepository.AddEndTimeBarrow(bookId, newEndTimeBarrow);
        }

    }
}
