namespace Winterhold.Presentation.Web.ViewModels
{
    public class BookIndexViewModel
    {
        public string? Name { get; set; }

        public string? Title { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }

        public bool IsAvailable { get; set; } = false;
        public List<BookViewModel> Books { get; set; }

        public string Category { get; set; }
    }
}
