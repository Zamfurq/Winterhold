using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using Winterhold.Business.Interfaces;
using Winterhold.Business.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) { 
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        public BookIndexViewModel GetAllBooks(int pageNumber, int pageSize, string? name, string? title, string category, bool isAvailable)
        {
            List<BookViewModel> books = _bookRepository.GetBooks(pageNumber,pageSize,title,name,category, isAvailable)
            .Select(b => new BookViewModel
                {
                    Code = b.Code,
                    Title = b.Title,
                    TotalPage = b.TotalPage,
                    AuthorId = b.AuthorId,
                    CategoryName = b.CategoryName,
                    Summary = b.Summary,
                    ReleaseDate = b.ReleaseDate,
                    IsBorrowed = b.IsBorrowed,
            })
            .ToList();

            int totalItem = _bookRepository.CountBooks(title, name, category, isAvailable);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new BookIndexViewModel
            {
                Name = name,
                Title = title,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Books = books,
                Category = category
            };
        }

        public BookViewModel GetBookbyCode(string code)
        {
            var book = _bookRepository.GetBookByCode(code);
            return new BookViewModel
            {
                Code = book.Code,
                Title = book.Title,
                CategoryName = book.CategoryName,
                AuthorId = book.AuthorId,
                ReleaseDate = book.ReleaseDate,
                TotalPage = book.TotalPage,
                Summary = book.Summary,

            };
        }

        public void Insert(BookViewModel book)
        {
            
           
            Book newBook = new Book
            {
                Code = book.Code,
                Title = book.Title,
                AuthorId = book.AuthorId,
                ReleaseDate = book.ReleaseDate,
                TotalPage = book.TotalPage,
                Summary = book.Summary,
                CategoryName = book.CategoryName
            };
            _bookRepository.InsertBook(newBook);
        }

        public void Update(BookViewModel book)
        {
            Book newBook = new Book
            {
                Code = book.Code,
                Title = book.Title,
                AuthorId = book.AuthorId,
                ReleaseDate = book.ReleaseDate,
                TotalPage = book.TotalPage,
                Summary = book.Summary,
                CategoryName = book.CategoryName
            };
            _bookRepository.UpdateBook(newBook);
        }

        public void Delete(BookViewModel book)
        {
            Book theBook = _bookRepository.GetBookByCode(book.Code);

            _bookRepository.DeleteBook(theBook);
        }

        public List<SelectListItem> GetAuthors()
        {
            var authors = _authorRepository.GetAuthors();

            var selectListItems = authors.OrderBy(a => a.FirstName)
                .Select(a => new SelectListItem {  Value = a.Id.ToString(), Text = (a.Title  + " " + a.FirstName +  " " + a.LastName)})
                .ToList();

            return selectListItems;
        }

        public string GenerateCode(string category)
        {
            int bookCount = _bookRepository.GetBooksIDByCategory(category);
            string bookCode = "";
            if (bookCount == 0)
            {
                bookCode = "B/" + category + "/" + 1;
            }
            else
            {
                bookCode = "B/" + category + "/" + (bookCount + 1);
            }

            return bookCode;
        }


    }
}
