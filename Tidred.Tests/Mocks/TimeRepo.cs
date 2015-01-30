using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tidred.Repo;

namespace Tidred.Tests.Mocks
{
  public class TimeRepo : BaseRepoMock<TimeEntry>, ITimeRepository
  {
    public IEnumerable<TimeEntry> GetAllEntries(string userId)
    {
        return TestData.CreateTimeEntries().Where(e => e.UserId == userId);
    }

    public IEnumerable<TimeEntry> GetAllProjectEntries(long projectId)
    {
        return TestData.CreateTimeEntries().Where(e => e.ProjectId == projectId);
    }

    public IEnumerable<PriceType> GetAllPriceTypes(int coId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TimeEntry> GetAllCustomerEntries(long customerId)
    {
        return TestData.CreateTimeEntries().Where(e => e.CustomerId == customerId);
    }

    public TimeEntry GetEntry(long id)
    {
        return TestData.CreateTimeEntries().Single(e => e.TimeEntryId == id);
    }



    public IEnumerable<TimeEntry> GetEntries(string userId, DateTime startDate, DateTime endDate, long? customerId, long? projectId)
    {
        throw new NotImplementedException();
    }
  }
}
