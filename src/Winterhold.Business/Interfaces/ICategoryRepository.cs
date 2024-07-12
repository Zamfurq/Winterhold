using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategory(int pageNumber,int pageSize,string? name);

        public Category GetCategoryByName(string name);

        public int CountCategory(string? name);

        public void InsertCategory(Category category);

        public void UpdateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}
