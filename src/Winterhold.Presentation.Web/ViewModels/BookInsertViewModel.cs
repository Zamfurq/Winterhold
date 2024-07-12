using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winterhold.Presentation.Web.ViewModels
{
    public class BookInsertViewModel
    {
        public BookViewModel Book { get; set; } = new BookViewModel();

        public string Category { get; set; } = null!;
        public List<SelectListItem>? Author { get; set; } = null;
    }
}
