using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winterhold.Presentation.Web.ViewModels
{
    public class LoanIndexViewModel
    {
        public string? BookTitle { get; set; }

        public string? CustomerName { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }

        public bool IsDue { get; set; }
        public List<LoanViewModel> Loans { get; set; }

        public List<SelectListItem>? Customers { get; set; } = null;

        public List<SelectListItem>? Books { get; set; } = null;


    }
}
