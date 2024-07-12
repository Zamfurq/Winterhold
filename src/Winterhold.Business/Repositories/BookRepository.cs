using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Business.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly WinterholdContext _dbContext;

        public BookRepository(WinterholdContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Book> GetBooks(int pageNumber, int pageSize, string? title, string? name, string category, bool isAvailable)
        {
            var query = from book in _dbContext.Books
                        join author in _dbContext.Authors on book.AuthorId equals author.Id
                        where book.CategoryName.Equals(category) && (title == null || book.Title.Contains(title))
                        && (name == null || author.FirstName.Contains(name) || author.LastName.Contains(name))
                        && (isAvailable == false || book.IsBorrowed == false)
                        select book;



            return query.ToList();
        }

        public Book GetBookByCode(string code)
        {
            return _dbContext.Books.FirstOrDefault(b => b.Code == code) ??
                throw new NullReferenceException("Book is not found");
        }

        

        public void InsertBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            if (book.Code == null)
            {
                throw new ArgumentNullException("Book Code is empty");
            }

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }

        public int CountBooks(string? title, string? name, string category, bool isAvailable)
        {
            var query = from book in _dbContext.Books
                        join author in _dbContext.Authors on book.AuthorId equals author.Id
                        where book.CategoryName.Equals(category) && (title == null || book.Title.Contains(title))
                        && (name == null || author.FirstName.Contains(name) || author.LastName.Contains(name))
                        && (isAvailable == false || book.IsBorrowed == false)
                        select book;

            return query.Count();
        }

        public void DeleteBook(Book book)
        {
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

        public List<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
        }

        public int GetBooksIDByCategory(string category)
        {
            var books = from book in _dbContext.Books
                           where book.CategoryName == category
                                    select book;
            return books.Count();
        }
    }
}
