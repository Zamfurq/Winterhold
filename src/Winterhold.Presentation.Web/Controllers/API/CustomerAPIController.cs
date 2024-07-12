using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;

namespace Winterhold.Presentation.Web.Controllers.API
{

    [Route("Customer/api")]
    [ApiController]
    public class CustomerAPIController : Controller
    {
        private readonly CustomerService _service;

        public CustomerAPIController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet("{number}")]
        public IActionResult Index(string number)
        {
            var vm = _service.GetCustomerByCode(number);
            return Ok(vm);
        }
    }
}
