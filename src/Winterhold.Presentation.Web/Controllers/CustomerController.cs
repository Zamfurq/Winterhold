using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers
{
    [Route("Customer")]
    public class CustomerController : Controller
    {

        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(bool isExpired,int page = 1, string? customerName = "", string? customerNumber = "")
        {
            int pageSize = 10;
            var vm = _service.GetAllCustomer(page, pageSize,customerName,customerNumber, isExpired);
            return View(vm);
        }

        [HttpGet("insert")]
        public IActionResult Insert() {
            return View("Upsert");
        }

        [HttpPost("insert")]
        public IActionResult Insert(CustomerViewModel vm) {
            if (!ModelState.IsValid)
            {
                return View("Upsert", vm);
            }
            _service.Insert(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("update/{number}")]
        public IActionResult Update(string number)
        {
            var vm = _service.GetCustomerByCode(number);
            return View("Upsert", vm);
        }

        [HttpPost("update/{number}")]
        public IActionResult Update(CustomerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Upsert", vm);
            }
            _service.Update(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{number}")]
        public IActionResult Delete(string number)
        {
            var vm = _service.GetCustomerByCode(number);
            _service.Delete(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("extend/{number}")]
        public IActionResult Extend(string number)
        {
            _service.Extend(number);
            return RedirectToAction("Index");
        }
    }
}
