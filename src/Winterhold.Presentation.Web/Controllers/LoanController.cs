using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;

namespace Winterhold.Presentation.Web.Controllers
{
    [Route("Loan")]
    public class LoanController : Controller
    {
        private readonly LoanService _service;

        public LoanController(LoanService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(bool isDue,int page = 1, string? bookTitle = "", string? customerName = "")
        {
            int pageSize = 10;
            var vm = _service.GetAllLoan(page, pageSize, bookTitle,customerName, isDue); 
            return View(vm);
        }
    }
}
