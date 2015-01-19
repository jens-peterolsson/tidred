using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Services
{
    public class ReportService : IReportService
    {
        private readonly ICustomerRepository _customerRepo = RepositoryFactory.Instance.CreateCustomerRepository();
        private readonly ITimeRepository _timeRepo = RepositoryFactory.Instance.CreateTimeRepository();
        private readonly IProjectRepository _projectRepo = RepositoryFactory.Instance.CreateProjectRepository();

        private const string FormatWithDecimals = "{0:#,0.00}";
        private const string FormatWithoutDecimals = "{0:#,0}";

        public Dictionary<string, string> CustomerTotals(long customerId, DateTime startDate, DateTime endDate)
        {
            var customer = _customerRepo.GetCustomer(customerId);

            // we'll ned all customer entries for this...
            var customerEntries = _timeRepo.GetAllCustomerEntries(customerId).Where(entry =>
                entry.Day >= startDate && entry.Day <= endDate).ToList();

            // antal timmar
            var noOfHours = customerEntries.Sum(entry => entry.Hours);

            // fixed projects
            var customerFixedProjects = _projectRepo.GetAllProjects(customer.CoID).Where(project => project.CustomerId == customerId && project.IsFixedPrice);
            var fixedPriceProjects =
              customerEntries.Where(entry => customerFixedProjects.Any(project => project.ProjectId == entry.ProjectId))
                .Select(entry => entry.Project).GroupBy(project => project.ProjectId).Select(group => group.First());

            var fixedAmount = fixedPriceProjects.Sum(project => project.ProjectFixedPrice.Price);

            // get running amounts
            var runningEntries =
                customerEntries.Where(entry => entry.ProjectId == null ||
                  customerFixedProjects.All(project => project.ProjectId != entry.ProjectId));
            var runningAmount =
                  runningEntries.Sum(entry => entry.Hours * entry.Rate);

            var totalAmount = runningAmount + fixedAmount;
            var perHour = totalAmount / noOfHours;

            var result = new Dictionary<string, string>
        {
           {ReportConstants.Customer, customer.Name},
           {ReportConstants.TotalAmount, string.Format(FormatWithoutDecimals, totalAmount)},
           {ReportConstants.NoOfHours, string.Format(FormatWithDecimals, noOfHours)},
           {ReportConstants.AmountPerHour, string.Format(FormatWithoutDecimals,  perHour)}
        };

            AddDateSelection(startDate, endDate, result);

            return result;
        }

        public Dictionary<string, string> ProjectSummary(long projectId, DateTime startDate, DateTime endDate)
        {
            // get project entries
            var project = _projectRepo.GetProject(projectId);
            var projectEntries = _timeRepo.GetAllProjectEntries(projectId).Where(entry =>
                                    entry.Day >= startDate && entry.Day <= endDate).ToList();

            var noOfHours = projectEntries.Sum(entry => entry.Hours);

            decimal amount = 0;
            if (project.IsFixedPrice)
            {
                amount = project.ProjectFixedPrice.Price;
            }
            else
            {
                amount = projectEntries.Sum(entry => entry.Hours * (entry.Rate ?? 0));
            }

            var perHour = amount / noOfHours;

            var result = new Dictionary<string, string>
        {
          {ReportConstants.Project, project.Name},
          {ReportConstants.Customer, project.Customer.Name},
          {ReportConstants.TotalAmount, string.Format(FormatWithoutDecimals,  amount)},
          {ReportConstants.TotalNoOfHours, string.Format(FormatWithDecimals,  noOfHours)},
          {ReportConstants.AmountPerHour, string.Format(FormatWithoutDecimals,  perHour)}
        };

            AddDateSelection(startDate, endDate, result);

            return result;
        }

        public Dictionary<string, string> WorkingHours(string userId, DateTime startDate, DateTime endDate)
        {
            var result = new Dictionary<string, string>();

            // get all entries within period
            var periodEntries = _timeRepo.GetAllEntries(userId).Where(entry => entry.Day >= startDate
                && entry.Day <= endDate).ToList();

            // add sum row with total hours
            result.Add(ReportConstants.TotalNoOfHours, string.Format(FormatWithDecimals, periodEntries.Sum(entry => entry.Hours)));
            // group hours by year and sort
            var yearSummary = periodEntries.GroupBy(entry => entry.Day.Year, entry => entry.Hours, (key, g) => new { Year = key, Hours = g.Sum() });
            foreach (var yearGroup in yearSummary)
            {
                result.Add(yearGroup.Year.ToString(CultureInfo.InvariantCulture), string.Format(FormatWithDecimals, yearGroup.Hours));

                var year = yearGroup.Year;
                var monthSummary = periodEntries.Where(entry => entry.Day.Year == year).GroupBy(entry => entry.Day.ToString("yyyy-MM"), entry => entry.Hours, (key, g) => new { Month = key, Hours = g.Sum() }).OrderBy(x => x.Month);
                foreach (var monthGroup in monthSummary)
                {
                    result.Add(monthGroup.Month, string.Format(FormatWithDecimals, monthGroup.Hours));
                }
            }

            AddDateSelection(startDate, endDate, result);

            return result;
        }

        private void AddDateSelection(DateTime startDate, DateTime endDate, Dictionary<string, string> dict)
        {
            var selectionValue = startDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            dict.Add(ReportConstants.Period, selectionValue);
        }
    }
}
