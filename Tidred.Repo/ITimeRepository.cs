using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public interface ITimeRepository : IBaseRepo<TimeEntry>
    {
        IEnumerable<TimeEntry> GetAllEntries(string userId);
        IEnumerable<TimeEntry> GetAllProjectEntries(long projectId);
        IEnumerable<TimeEntry> GetAllCustomerEntries(long customerId);
        TimeEntry GetEntry(long id);
    }
}
