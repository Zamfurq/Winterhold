using Winterhold.Business.Interfaces;
using Winterhold.Business.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryIndexViewModel GetAllCategory(int pageNumber, int pageSize, string? name)
        {
            List<CategoryViewModel> categories = _categoryRepository.GetCategory(pageNumber,pageSize,name)
                .Select(c => new CategoryViewModel { Name = c.Name, Bay = c.Bay, Floor = c.Floor, Isle = c.Isle}).ToList();

            int totalItem = _categoryRepository.CountCategory(name);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new CategoryIndexViewModel
            {
                Name = name,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Categories = categories
            };
        }

        public CategoryViewModel GetCategoryByName(string name)
        {
            Category category = _categoryRepository.GetCategoryByName(name);
            return new CategoryViewModel
            {
                Name = category.Name,
                Bay = category.Bay,
                Isle = category.Isle,
                Floor = category.Floor
            };
        }

        public void InsertCategory(CategoryViewModel category)
        {
            Category newCategory = new Category
            {
                Name = category.Name,
                Bay = category.Bay,
                Floor = category.Floor,
                Isle = category.Isle
            };
            _categoryRepository.InsertCategory(newCategory);
        }

        public void UpdateCategory(string name,CategoryViewModel newCategory)
        {
            Category theCategory = _categoryRepository.GetCategoryByName(name);
            theCategory.Name = newCategory.Name;
            theCategory.Isle = newCategory.Isle;
            theCategory.Floor = newCategory.Floor;
            theCategory.Bay = newCategory.Bay;


            _categoryRepository.UpdateCategory(theCategory);
        }

        public void DeleteCategory(CategoryViewModel category)
        {
            Category newCategory = _categoryRepository.GetCategoryByName(category.Name);
            _categoryRepository.DeleteCategory(newCategory);
        }
    }
}
