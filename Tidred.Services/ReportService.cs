﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tidred.Repo;

namespace Tidred.Services
{
    public class ReportService : IReportService
    {
        private readonly ICustomerRepository _customerRepo = RepoFactory.Instance.CreateCustomerRepo();
        private readonly ITimeRepository _timeRepo = RepoFactory.Instance.CreateTimeRepo();
        private readonly IProjectRepository _projectRepo = RepoFactory.Instance.CreateProjectRepo();
        private readonly IUserRepository _userRepo = RepoFactory.Instance.CreateUserRepo();

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

            decimal amount;
            if (project.IsFixedPrice)
            {
                amount = project.ProjectFixedPrice.Price;
            }
            else
            {
                amount = projectEntries.Sum(entry => entry.Hours * (entry.Rate ?? 0));
            }

            decimal perHour = 0;
            if(noOfHours != 0)
            {
                perHour = amount / noOfHours;
            }

            var result = new Dictionary<string, string>
            {
              {ReportConstants.Project, project.Name},
              {ReportConstants.Customer, project.Customer.Name},
              {ReportConstants.TotalAmount, string.Format(FormatWithoutDecimals, amount)},
              {ReportConstants.TotalNoOfHours, string.Format(FormatWithDecimals, noOfHours)},
              {ReportConstants.AmountPerHour, string.Format(FormatWithoutDecimals, perHour)}
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


        public Dictionary<string, string> FlexResult(string userId, int coId, DateTime startDate, DateTime endDate, long? customerId)
        {
            var result = new Dictionary<string, string>();

            // get all entries within period
            var periodEntries = _timeRepo.GetEntries(userId, startDate, endDate, customerId, null).ToList();
            var schedules = _userRepo.GetWorkingSchedules(coId).ToList();

            var flex = 0m;
            var indexDate = startDate;
            
            while (indexDate <= endDate)
            {
                var normalHours = 0m;

                if (indexDate.DayOfWeek != DayOfWeek.Saturday && indexDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    var schedule = schedules.Single(s => s.StartMonth <= indexDate.Month && s.EndMonth >= indexDate.Month);
                    normalHours = schedule.WorkingHours;
                }

                var workedHours =
                    periodEntries.Where(entry => entry.Day.Date == indexDate.Date).Sum(entry => entry.Hours);
                
                flex += workedHours - normalHours;
                indexDate = indexDate.AddDays(1);
            }

            result.Add(ReportConstants.Flex, string.Format(FormatWithDecimals, flex));
            return result;
        }
    }
}
