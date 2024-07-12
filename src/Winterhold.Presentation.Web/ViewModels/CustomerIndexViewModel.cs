namespace Winterhold.Presentation.Web.ViewModels
{
    public class CustomerIndexViewModel
    {
        public string? CustomerNumber { get; set; }

        public string? CustomerName { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }

        public bool IsExpired { get; set; } = false;
        public List<CustomerViewModel> Customers { get; set; }
    }
}
