using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tidred.Repo;

namespace Tidred.Tests.Mocks
{
  public class TimeRepo : BaseRepo<TimeEntry>, ITimeRepository
  {
    public IEnumerable<TimeEntry> GetAllEntries(string userId)
    {
        return TestData.CreateTimeEntries().Where(e => e.UserId == userId);
    }

    public IEnumerable<TimeEntry> GetAllProjectEntries(long projectId)
    {
        return TestData.CreateTimeEntries().Where(e => e.ProjectId == projectId);
    }

    public IEnumerable<TimeEntry> GetAllCustomerEntries(long customerId)
    {
        return TestData.CreateTimeEntries().Where(e => e.CustomerId == customerId);
    }

    public TimeEntry GetEntry(long id)
    {
        return TestData.CreateTimeEntries().Single(e => e.TimeEntryId == id);
    }

  }
}
