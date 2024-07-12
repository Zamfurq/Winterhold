using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Interfaces
{
    public interface ILoanRepository
    {
        public List<Loan> GetLoan(string? bookTitle, string? customerName, int pageNumber, int pageSize, bool isDue);

        public int CountLoan(string? bookTitle, string? customerName, bool isDue);

        public Loan GetLoanById(long id);

        public void InsertLoan(Loan loan);

        public void UpdateLoan(Loan loan);

    }
}
