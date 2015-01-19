using System.Collections.Generic;
using System.Linq;

namespace Tidred.Repo
{
    public class CustomerRepository : BaseRepo<Customer>, ICustomerRepository
    {
        private readonly TidredContext _context = new TidredContext();

        public IEnumerable<Customer> GetAllCustomers(int coId)
        {
            return _context.Customers.Where(c => c.CoID == coId); ;
        }

        public Customer GetCustomer(long customerId)
        {
            return _context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
        }
    }
}
