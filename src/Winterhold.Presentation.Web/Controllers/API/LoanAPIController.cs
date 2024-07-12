using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers.API
{
    [Route("Loan/api")]
    [ApiController]
    public class LoanAPIController : Controller
    {
        private readonly LoanService _service;

        public LoanAPIController(LoanService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Index(long id) {
            var vm = _service.GetLoanById(id);
            return Ok(vm);
        }

        [HttpPost]
        public IActionResult Insert(LoanViewModel vm)
        {
            _service.Insert(vm);
            return Ok(vm);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id,LoanViewModel vm)
        {
            _service.Update(id,vm);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public IActionResult Return(long id)
        {
            _service.Return(id);
            return Ok();
        }
    }
}
