namespace Winterhold.Presentation.Web.ViewModels
{
    public class CategoryIndexViewModel
    {
        public string? Name { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
