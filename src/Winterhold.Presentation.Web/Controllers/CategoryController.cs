using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers
{
    [Route("Book")]
    public class CategoryController : Controller
    {

        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, string? name = "")
        {
            int pageSize = 10;
            var vm = _service.GetAllCategory(page, pageSize, name);
            return View(vm);
        }


    }
}
