using System.Collections.Generic;
using System.Linq;

namespace Tidred.Repo
{
    public class CustomerRepository : BaseRepo<Customer>, ICustomerRepository
    {
        public IEnumerable<Customer> GetAllCustomers(int coId)
        {
            return Context.Customers.Where(c => c.CoID == coId); ;
        }

        public Customer GetCustomer(long customerId)
        {
            return Context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
        }

        public IEnumerable<Currency> GetAllCurrencies()
        {
            return Context.Currencies; ;
        }

    }
}
