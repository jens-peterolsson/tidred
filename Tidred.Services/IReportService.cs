using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Services
{
    public interface IReportService
    {
        Dictionary<string, string> CustomerTotals(long customerId, DateTime startDate, DateTime endDate);
        Dictionary<string, string> ProjectSummary(long projectId, DateTime startDate, DateTime endDate);
        Dictionary<string, string> WorkingHours(string userId, DateTime startDate, DateTime endDate);
        Dictionary<string, string> FlexResult(string userId, int coId, DateTime startDate, DateTime endDate, long? customerId);
    }
}
