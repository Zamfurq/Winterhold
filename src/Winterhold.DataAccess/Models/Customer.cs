using System;
using System.Collections.Generic;

namespace Winterhold.DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Loans = new HashSet<Loan>();
        }

        public string MembershipNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime MembershipExpireDate { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
