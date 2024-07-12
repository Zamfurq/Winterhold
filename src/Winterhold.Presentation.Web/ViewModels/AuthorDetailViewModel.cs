namespace Winterhold.Presentation.Web.ViewModels
{
    public class AuthorDetailViewModel
    {
        public AuthorViewModel Author { get; set; } = new AuthorViewModel();

        public BookIndexViewModel BookIndex { get; set; } = new BookIndexViewModel();
    }
}
