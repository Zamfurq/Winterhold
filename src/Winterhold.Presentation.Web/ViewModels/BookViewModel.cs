using System.ComponentModel.DataAnnotations;

namespace Winterhold.Presentation.Web.ViewModels
{
    public class BookViewModel
    {
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string CategoryName { get; set; } = null!;

        [Required]
        public long AuthorId { get; set; }
        public bool IsBorrowed { get; set; }
        public string? Summary { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? TotalPage { get; set; }

        
    }
}
