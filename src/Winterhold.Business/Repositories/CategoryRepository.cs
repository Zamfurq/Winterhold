using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Business.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WinterholdContext _dbcontext;

        public CategoryRepository(WinterholdContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public List<Category> GetCategory(int pageNumber, int pageSize, string? name)
        {
            var query = from category in _dbcontext.Categories
                        where name == null || category.Name.Contains(name)
                        select category;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Category GetCategoryByName(string name)
        {
            return _dbcontext.Categories.FirstOrDefault(c => c.Name == name) ??
                throw new NullReferenceException("Category is not found");
        }

        public int CountCategory(string? name)
        {
            var query = from category in _dbcontext.Categories
                        where name == null || category.Name.Contains(name)
                        select category;

            return query.Count();
        }

        public void InsertCategory(Category category)
        {
            _dbcontext.Categories.Add(category);
            _dbcontext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            if (category.Name == null)
            {
                throw new ArgumentNullException("Category name is empty");
            }
            _dbcontext.Categories.Update(category);
            _dbcontext.SaveChanges();
        }
        

        public void DeleteCategory(Category category)
        {
            _dbcontext.Categories.Remove(category); 
            _dbcontext.SaveChanges();
        }
    }
}
