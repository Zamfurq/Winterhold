using Microsoft.AspNetCore.Mvc;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers
{
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }

        [HttpGet("{category}")]
        public IActionResult Index(string category, bool isAvailable, int page = 1, string? name = "", string? title = "")
        {
            int pageSize = 10;
            var vm = _service.GetAllBooks(page, pageSize, name, title, category, isAvailable);
            return View(vm);
        }

        [HttpGet("{category}/insert")]
        public IActionResult Insert(string category)
        {

            return View("Upsert", new BookInsertViewModel { Author = _service.GetAuthors(), Category = category, Book = new BookViewModel { Code = _service.GenerateCode(category) } });
        }

        [HttpPost("{category}/insert")]
        public IActionResult Insert(BookInsertViewModel vm, string category)
        {
            if (!ModelState.IsValid)
            {
                vm.Author = _service.GetAuthors();
                vm.Category = category;
                return View("Upsert", vm);
            }
            _service.Insert(vm.Book);
            return RedirectToAction("Index", new { category });
        }

        [HttpGet("{category}/update/{code}")]
        public IActionResult Update(string category, string code)
        {
            var vm = _service.GetBookbyCode(code);
            return View("Upsert",  new BookInsertViewModel { Author = _service.GetAuthors(), Book = vm, Category = category });
        }

        [HttpPost("{category}/update/{code}")]
        public IActionResult Update(BookInsertViewModel vm, string category)
        {
            if (!ModelState.IsValid)
            {
                vm.Author = _service.GetAuthors();
                vm.Category = category;
                
                return View("Upsert", vm);
            }
            _service.Update(vm.Book);
            return RedirectToAction("Index",new {category});
        }

        [HttpGet("{category}/delete/{code}")]
        public IActionResult Delete(string category, string code)
        {
            var vm = _service.GetBookbyCode(code);
            _service.Delete(vm);
            return RedirectToAction("Index", new { category });
        }
    }
}
