using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Business.Interfaces
{
    public interface ICustomerRepository
    {
        public List<Customer> GetCustomer(string? name,string? number, int pageNumber, int pageSize, bool isExpired);

        public int CountCustomer(string? name, string? number, bool isExpired);

        public Customer GetCustomeryByCode(string code);

        public void InsertCustomer(Customer customer);

        public void UpdateCustomer(Customer customer);

        public void DeleteCustomer(Customer customer);

        public List<Customer> GetCustomers();
    }
}
