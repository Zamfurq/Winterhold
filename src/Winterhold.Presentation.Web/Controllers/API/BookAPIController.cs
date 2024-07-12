using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;

namespace Winterhold.Presentation.Web.Controllers.API
{
    [Route("Book/api")]
    [ApiController]
    public class BookAPIController : Controller
    {
        private readonly BookService _service;

        public BookAPIController(BookService service)
        {
            _service = service;
        }

        [HttpGet("{code}")]
        public IActionResult Index(string code)
        {
            var vm = _service.GetBookbyCode(code);
            return Ok(vm);
        }
    }
}
