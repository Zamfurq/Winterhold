using System.ComponentModel.DataAnnotations;
using Winterhold.DataAccess.Models;

namespace Winterhold.Presentation.Web.ViewModels
{
    public class LoanViewModel
    {
        public long Id { get; set; }

        [Required]
        public string CustomerNumber { get; set; } = null!;

        [Required]
        public string BookCode { get; set; } = null!;

        [Required]
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Note { get; set; }

        public Book? Book { get; set; }

        public Customer? Customer { get; set; }
    }
}
