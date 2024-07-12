using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers
{
    [Route("Author")]
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1,string? name = "")
        {
            int pageSize = 10;
            var vm = _authorService.GetAllAuthor(page,pageSize,name);
            return View(vm);
        }

        [HttpGet("insert")]
        public IActionResult Insert()
        {
            return View("Upsert");
        }

        [HttpPost("insert")]
        public IActionResult Insert(AuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Upsert",vm);
            }
            _authorService.Insert(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("update/{id}")]
        public IActionResult Update(long id) {
            var vm = _authorService.GetAuthorById(id);
            return View("Upsert",vm);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(AuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Upsert",vm);
            }
            _authorService.Update(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(long id)
        {
            var vm = _authorService.GetAuthorById(id);
            _authorService.Delete(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(long id, int page = 1)
        {
            int pageSize = 10; 
            var vm = _authorService.GetAuthorById(id);
            BookIndexViewModel bookVm = _authorService.GetBooks(vm,page,pageSize);
            return View("Detail",new AuthorDetailViewModel { Author = vm, BookIndex = bookVm });
        }
    }
}
