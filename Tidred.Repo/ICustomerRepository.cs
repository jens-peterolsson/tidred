using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public interface ICustomerRepository : IBaseRepo<Customer>
    {
        IEnumerable<Customer> GetAllCustomers(int coId);
        Customer GetCustomer(long customerId);
    }
}
