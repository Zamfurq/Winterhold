using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers.API
{
    [Route("Category")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryAPIController(CategoryService service)
        {
            _service = service;
        }


        

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var vm = _service.GetCategoryByName(name);
            return Ok(vm);
        }

        [HttpPost]
        public IActionResult Insert(CategoryViewModel vm)
        {
            _service.InsertCategory(vm);
            return Ok(vm);
        }

        [HttpPut("{name}")]
        public IActionResult Update(string name, CategoryViewModel vm)
        {
            _service.UpdateCategory(name,vm);
            return Ok(vm);
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var vm = _service.GetCategoryByName(name);
            if (vm == null)
            {
                return NotFound();
            }
            _service.DeleteCategory(vm);
            return Ok();
        }
    }
}
