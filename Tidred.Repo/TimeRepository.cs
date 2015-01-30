using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Tidred.Repo
{
    public class TimeRepository : BaseRepo<TimeEntry>, ITimeRepository
    {
        public IEnumerable<TimeEntry> GetAllEntries(string userId)
        {
            return Context.TimeEntries.Where(e => e.UserId == userId);
        }

        public TimeEntry GetEntry(long id)
        {
            return Context.TimeEntries.SingleOrDefault(e => e.TimeEntryId == id);
        }

        public IEnumerable<PriceType> GetAllPriceTypes(int coId)
        {
            return Context.PriceTypes.Where(p => p.CoID == coId);
        }

        public IEnumerable<TimeEntry> GetAllProjectEntries(long projectId)
        {
            return Context.TimeEntries.Where(e => e.ProjectId == projectId);
        }

        public IEnumerable<TimeEntry> GetAllCustomerEntries(long customerId)
        {
            return Context.TimeEntries.Where(e => e.CustomerId == customerId);
        }

    }
}
