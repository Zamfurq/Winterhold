using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Interfaces
{
    public interface IAuthorRepository
    {
        public List<Author> GetAuthors(int pageNumber, int pageSize,string? name);

        public int CountAuthor(string? name);

        public Author GetAuthorById(long id);

        public void InsertAuthor(Author author);

        public void UpdateAuthor(Author author);    

        public void DeleteAuthor(Author author);

        public List<Author> GetAuthors();

        public List<Book> GetBooksByAuthor(long id, int pageNumber, int pageSize);

        public int CountBooksByAuthor(long id);
    }
}
