using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Business.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WinterholdContext _dbcontext;

        public CustomerRepository(WinterholdContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        

        public List<Customer> GetCustomer(string? name, string? number, int pageNumber, int pageSize, bool isExpired)
        {
            var query = from customer in _dbcontext.Customers
                        where (name == null || customer.FirstName.Contains(name) || customer.LastName.Contains(name))
                        && (number == null || customer.MembershipNumber.Contains(number))
                        && (isExpired == false || customer.MembershipExpireDate < DateTime.Now)
                        select customer;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Customer GetCustomeryByCode(string code)
        {
            return _dbcontext.Customers.FirstOrDefault(c => c.MembershipNumber == code)
                ?? throw new NullReferenceException("Customer is not found");
        }

        public void InsertCustomer(Customer customer)
        {
            _dbcontext.Customers.Add(customer);
            _dbcontext.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer.MembershipNumber == null)
            {
                throw new ArgumentNullException("Customer number is empty");
            }

            _dbcontext.Customers.Update(customer);
            _dbcontext.SaveChanges();
        }

        public int CountCustomer(string? name, string? number, bool isExpired)
        {
            var query = from customer in _dbcontext.Customers
                        where (name == null || customer.FirstName.Contains(name) || customer.LastName.Contains(name))
                        && (number == null || customer.MembershipNumber.Contains(number))
                        && (isExpired == false || customer.MembershipExpireDate > DateTime.Now)
                        select customer;

            return query.Count();
        }

        public void DeleteCustomer(Customer customer)
        {
            _dbcontext.Customers.Remove(customer);
            _dbcontext.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            return _dbcontext.Customers.ToList();
        }
    }
}
