using Winterhold.Business.Interfaces;
using Winterhold.Business.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CustomerIndexViewModel GetAllCustomer(int pageNumber, int pageSize, string? name, string? number, bool isExpired)
        {
            List<CustomerViewModel> customers = _customerRepository.GetCustomer(name,number,pageNumber,pageSize, isExpired)
                .Select(c => new CustomerViewModel { 
                    MembershipNumber = c.MembershipNumber,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    BirthDate = c.BirthDate,
                    Gender = c.Gender,
                    Phone = c.Phone,
                    MembershipExpireDate = c.MembershipExpireDate
                })
                .ToList();

            int totalItem = _customerRepository.CountCustomer(name, number, isExpired);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new CustomerIndexViewModel
            {
                CustomerName = name,
                CustomerNumber = number,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Customers = customers
            };
        }

        public CustomerViewModel GetCustomerByCode(string code)
        {
            var customer = _customerRepository.GetCustomeryByCode(code);
            return new CustomerViewModel
            {
                MembershipNumber = customer.MembershipNumber,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                BirthDate = customer.BirthDate,
                Gender = customer.Gender,
                Phone = customer.Phone,
                MembershipExpireDate = customer.MembershipExpireDate
            };
        }

        public void Insert(CustomerViewModel customer)
        {
            Customer newCustomer = new Customer
            {
                MembershipNumber = customer.MembershipNumber,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                BirthDate = customer.BirthDate,
                Gender = customer.Gender,
                Phone = customer.Phone,
                MembershipExpireDate = DateTime.Now.AddYears(2)
            };
            _customerRepository.InsertCustomer(newCustomer);
        }

        public void Update(CustomerViewModel customer)
        {
            Customer newCustomer = _customerRepository.GetCustomeryByCode(customer.MembershipNumber);

            newCustomer.MembershipNumber = customer.MembershipNumber;
            newCustomer.FirstName = customer.FirstName;
            newCustomer.LastName = customer.LastName;
            newCustomer.Address = customer.Address;
            newCustomer.BirthDate = customer.BirthDate;
            newCustomer.Gender = customer.Gender;
            newCustomer.Phone = customer.Phone;

            _customerRepository.UpdateCustomer(newCustomer);
        }

        public void Delete(CustomerViewModel customer)
        {
            Customer theCustomer = _customerRepository.GetCustomeryByCode(customer.MembershipNumber);
            _customerRepository.DeleteCustomer(theCustomer);
        }

        public void Extend(string code)
        {
            Customer customer = _customerRepository.GetCustomeryByCode(code);
            customer.MembershipExpireDate = customer.MembershipExpireDate.AddYears(2);
            _customerRepository.UpdateCustomer(customer);
        }
    }
}
