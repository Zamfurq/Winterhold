using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Business.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly WinterholdContext _dbcontext;

        public AuthorRepository(WinterholdContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<Author> GetAuthors(int pageNumber, int pageSize,string? name)
        {
            var query = from author in _dbcontext.Authors
                        where name == null || author.FirstName.Contains(name) || author.LastName.Contains(name)
                        select author;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountAuthor(string? name)
        {
            var query = from author in _dbcontext.Authors
                        where name == null || author.FirstName.Contains(name) || author.LastName.Contains(name)
                        select author;
            return query.Count();
        }

        public Author GetAuthorById(long id)
        {
            return _dbcontext.Authors.FirstOrDefault(a => a.Id == id) ??
                throw new NullReferenceException("Author is not found");
        }


        public void InsertAuthor(Author author)
        {
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            if (author.Id == 0)
            {
                throw new ArgumentNullException("Author Id is empty");
            } 

            _dbcontext.Authors.Update(author);
            _dbcontext.SaveChanges();
        }
        public void DeleteAuthor(Author author)
        {
            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }

        public List<Book> GetBooksByAuthor(long id, int pageNumber, int pageSize)
        {
            var query = from book in _dbcontext.Books
                        where book.AuthorId.Equals(id)
                        select book;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountBooksByAuthor(long id)
        {
            var query = from book in _dbcontext.Books
                        where book.AuthorId.Equals(id)
                        select book;

            return query.Count();
        }

        public List<Author> GetAuthors()
        {
            return _dbcontext.Authors.ToList();
        }
    }
}
