using System.Xml.Linq;
using Winterhold.Business.Interfaces;
using Winterhold.Business.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public AuthorIndexViewModel GetAllAuthor(int pageNumber, int pageSize,string? name)
        {
            List<AuthorViewModel> authorVm = _authorRepository.GetAuthors(pageNumber,pageSize,name)
                                                .Select(a => new AuthorViewModel
                                                {
                                                    Id = a.Id,
                                                    Title = a.Title,
                                                    FirstName = a.FirstName,
                                                    LastName = a.LastName,
                                                    BirthDate = a.BirthDate,
                                                    DeceasedDate = a.DeceasedDate,
                                                    Education = a.Education,
                                                    Summary = a.Summary
                                                }).ToList();
            int totalItem = _authorRepository.CountAuthor(name);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem/ (decimal)pageSize);

            return new AuthorIndexViewModel
            {
                Name = name,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Author = authorVm
            };
        }

        public AuthorViewModel GetAuthorById(long id) {
            var author = _authorRepository.GetAuthorById(id);
            return new AuthorViewModel {
                Id = author.Id,
                Title = author.Title,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                DeceasedDate = author.DeceasedDate,
                Education = author.Education,
                Summary = author.Summary
            };
        }

        public void Insert(AuthorViewModel vm)
        {
            var author = new Author
            {
                Title = vm.Title,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                BirthDate = vm.BirthDate,
                DeceasedDate = vm.DeceasedDate,
                Education = vm.Education,
                Summary = vm.Summary
            };
            _authorRepository.InsertAuthor(author);
        }

        public void Update(AuthorViewModel vm)
        {
            var author = new Author
            {
                Id = vm.Id,
                Title = vm.Title,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                BirthDate = vm.BirthDate,
                DeceasedDate = vm.DeceasedDate,
                Education = vm.Education,
                Summary = vm.Summary
            };
            _authorRepository.UpdateAuthor(author);
        }

        public void Delete(AuthorViewModel vm)
        {
            var author = _authorRepository.GetAuthorById(vm.Id);
            _authorRepository.DeleteAuthor(author);
        }

        public BookIndexViewModel GetBooks(AuthorViewModel vm, int pageNumber, int pageSize)
        {
            List<BookViewModel> books = _authorRepository.GetBooksByAuthor(vm.Id, pageNumber, pageSize)
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

            int totalItem = _authorRepository.CountBooksByAuthor(vm.Id);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new BookIndexViewModel
            {
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Books = books
            };
        }
    }
}
