using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Winterhold.Business.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly WinterholdContext _dbcontext;

        public LoanRepository(WinterholdContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<Loan> GetLoan(string? bookTitle, string? customerName, int pageNumber, int pageSize, bool isDue)
        {
            var query = from loan in _dbcontext.Loans.Include(l => l.BookCodeNavigation).Include(l => l.CustomerNumberNavigation)
                        where (bookTitle == null || loan.BookCodeNavigation.Title.Contains(bookTitle))
                        && (customerName == null || loan.CustomerNumberNavigation.FirstName.Contains(customerName) || loan.CustomerNumberNavigation.LastName.Contains(customerName))
                        && (isDue == false || loan.DueDate < DateTime.Now)
                        select loan;

            List<Loan> result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (Loan loan in  result)
            {
                loan.BookCodeNavigation = _dbcontext.Books.FirstOrDefault(b => b.Code == loan.BookCode);
                loan.CustomerNumberNavigation = _dbcontext.Customers.FirstOrDefault(c => c.MembershipNumber == loan.CustomerNumber);
            }

            return result;
        }

        public Loan GetLoanById(long id)
        {
            return _dbcontext.Loans.FirstOrDefault(l => l.Id == id) ??
                throw new NullReferenceException("Loan is not found");
        }

        public void InsertLoan(Loan loan)
        {
            _dbcontext.Loans.Add(loan);
            _dbcontext.SaveChanges();
        }

        public void UpdateLoan(Loan loan)
        {
            if (loan.Id == 0)
            {
                throw new ArgumentNullException("Loan ID must be filled");
            }

            _dbcontext.Loans.Update(loan);
            _dbcontext.SaveChanges();

        }

        public int CountLoan(string? bookTitle, string? customerName, bool isDue)
        {
            var query = from loan in _dbcontext.Loans.Include(l => l.BookCodeNavigation).Include(l => l.CustomerNumberNavigation)
                        where (bookTitle == null || loan.BookCodeNavigation.Title.Contains(bookTitle))
                        && (customerName == null || loan.CustomerNumberNavigation.FirstName.Contains(customerName) || loan.CustomerNumberNavigation.LastName.Contains(customerName))
                        && (isDue == false || loan.DueDate < DateTime.Now)
                        select loan;

            return query.Count();
        }
    }
}
