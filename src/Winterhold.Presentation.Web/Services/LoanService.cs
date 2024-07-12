using Microsoft.AspNetCore.Mvc.Rendering;
using Winterhold.Business.Interfaces;
using Winterhold.Business.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;

        public LoanService(ILoanRepository loanRepository, 
                            ICustomerRepository customerRepository, 
                            IBookRepository bookRepository,
                            IAuthorRepository authorRepository,
                            ICategoryRepository categoryRepository)
        {
            _loanRepository = loanRepository;
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public LoanIndexViewModel GetAllLoan(int pageNumber, int pageSize, string? bookTitle, string? customerName, bool isDue)
        {
            List<LoanViewModel> loans = _loanRepository.GetLoan(bookTitle,customerName, pageNumber, pageSize, isDue)
                .Select(c => new LoanViewModel
                {
                    Id = c.Id,
                    BookCode = c.BookCode,
                    CustomerNumber = c.CustomerNumber,
                    LoanDate = c.LoanDate,
                    DueDate = c.DueDate,
                    ReturnDate = c.ReturnDate,
                    Note = c.Note,
                    Book = c.BookCodeNavigation,
                    Customer = c.CustomerNumberNavigation
                })
                .ToList();

            int totalItem = _loanRepository.CountLoan(bookTitle,customerName, isDue);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new LoanIndexViewModel
            {
                CustomerName = customerName,
                BookTitle = bookTitle,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Loans = loans,
                Customers = GetCustomers(),
                Books = GetBooks()
            };
        }

        public LoanViewModel GetLoanById(long id)
        {
            var loan = _loanRepository.GetLoanById(id);
            var customer = _customerRepository.GetCustomeryByCode(loan.CustomerNumber);
            
            return new LoanViewModel
            {
                Id = loan.Id,
                BookCode = loan.BookCode,
                CustomerNumber = loan.CustomerNumber,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Note = loan.Note
            };
        }

        public void Insert(LoanViewModel loan)
        {
            var book = _bookRepository.GetBookByCode(loan.BookCode);
            book.IsBorrowed = true;
            Loan newLoan = new Loan {
                BookCode = loan.BookCode,
                CustomerNumber = loan.CustomerNumber,
                LoanDate = loan.LoanDate,
                DueDate = loan.LoanDate.AddDays(5),
                Note = loan.Note,
                BookCodeNavigation = book,
                CustomerNumberNavigation = _customerRepository.GetCustomeryByCode(loan.CustomerNumber)
            };
            
            _loanRepository.InsertLoan(newLoan);
        }

        public void Update(long id,LoanViewModel loan)
        {
            var theLoan = _loanRepository.GetLoanById(id);
            var book = _bookRepository.GetBookByCode(theLoan.BookCode);
            book.IsBorrowed = false;
            theLoan.BookCode = loan.BookCode;
            theLoan.CustomerNumber = loan.CustomerNumber;
            theLoan.LoanDate = loan.LoanDate;
            theLoan.DueDate = loan.LoanDate.AddDays(5);
            theLoan.Note = loan.Note;
            theLoan.BookCodeNavigation = _bookRepository.GetBookByCode(loan.BookCode);
            theLoan.BookCodeNavigation.IsBorrowed = true;
            theLoan.CustomerNumberNavigation = _customerRepository.GetCustomeryByCode(loan.CustomerNumber);
            _loanRepository.UpdateLoan(theLoan);
        }

        public void Return(long id)
        {
            var theLoan = _loanRepository.GetLoanById(id);
            theLoan.ReturnDate = DateTime.Now;
            var book = _bookRepository.GetBookByCode(theLoan.BookCode);
            book.IsBorrowed = false;
            _loanRepository.UpdateLoan(theLoan);
        }

        public List<SelectListItem> GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();

            var selectListItems = customers.OrderBy(c => c.FirstName)
                .Select(c => new SelectListItem { Value = c.MembershipNumber, Text = (c.FirstName + " " + c.LastName) })
                .ToList();

            return selectListItems;
        }

        public List<SelectListItem> GetBooks()
        {
            var books = _bookRepository.GetBooks();

            var selectListItems = books.OrderBy(b => b.Title)
                .Select(b => new SelectListItem { Value = b.Code, Text = b.Title })
                .ToList();

            return selectListItems;
        }
    }
}
