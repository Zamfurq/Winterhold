using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Interfaces
{
    public interface IBookRepository
    {
        public List<Book> GetBooks(int pageNumber, int pageSize, string? title, string? name, string category, bool isAvailable);

        public Book GetBookByCode(string code);

        public int CountBooks(string? title, string? name,string category, bool isAvailable);

        public void InsertBook(Book book);

        public void UpdateBook(Book book);  

        public void DeleteBook(Book book);

        public List<Book> GetBooks();

        public int GetBooksIDByCategory(string category);
    }
}
