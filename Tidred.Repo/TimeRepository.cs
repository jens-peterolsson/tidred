using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Tidred.Repo
{
    public class TimeRepository : BaseRepo<TimeEntry>, ITimeRepository
    {
        private readonly TidredContext _context = new TidredContext();

        public IEnumerable<TimeEntry> GetAllEntries(string userId)
        {
            return _context.TimeEntries.Where(e => e.UserId == userId);
        }

        public TimeEntry GetEntry(long id)
        {
            return _context.TimeEntries.SingleOrDefault(e => e.TimeEntryId == id);
        }

        public IEnumerable<TimeEntry> GetAllProjectEntries(long projectId)
        {
            return _context.TimeEntries.Where(e => e.ProjectId == projectId);
        }

        public IEnumerable<TimeEntry> GetAllCustomerEntries(long customerId)
        {
            return _context.TimeEntries.Where(e => e.CustomerId == customerId);
        }

    }
}
