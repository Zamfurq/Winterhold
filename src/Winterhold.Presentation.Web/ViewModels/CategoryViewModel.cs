using System.ComponentModel.DataAnnotations;

namespace Winterhold.Presentation.Web.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "Category Name*")]
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Floor { get; set; }

        [Required]
        public string Isle { get; set; } = null!;

        [Required]
        public string Bay { get; set; } = null!;
    }
}
