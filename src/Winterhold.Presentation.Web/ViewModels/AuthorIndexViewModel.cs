namespace Winterhold.Presentation.Web.ViewModels
{
    public class AuthorIndexViewModel
    {
        public string? Name { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }
        public List<AuthorViewModel> Author { get; set;}
    }
}
