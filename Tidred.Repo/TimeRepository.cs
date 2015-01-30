using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Tidred.Repo
{
    public class TimeRepository : BaseRepo<TimeEntry>, ITimeRepository
    {
        public IEnumerable<TimeEntry> GetEntries(string userId, DateTime startDate, DateTime endDate, long? customerId, long? projectId)
        {
            var entries = GetAllEntries(userId)
                .Where(entry => entry.Day >= startDate && entry.Day <= endDate);

            if (customerId.HasValue && customerId > -1)
            {
                entries = entries.Where(entry => entry.CustomerId == customerId.Value);
            }

            if (projectId.HasValue && projectId > 0)
            {
                entries = entries.Where(entry => entry.ProjectId == projectId.Value);
            }

            return entries;
        }

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
