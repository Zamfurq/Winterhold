using System.ComponentModel.DataAnnotations;

namespace Winterhold.Presentation.Web.ViewModels
{
    public class AuthorViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime? DeceasedDate { get; set; }
        public string? Education { get; set; }
        public string? Summary { get; set; }
    }
}
