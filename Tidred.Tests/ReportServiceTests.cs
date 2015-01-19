using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tidred.Repo;
using Tidred.Services;

namespace Tidred.Tests
{
    [TestClass]
    public class ReportServiceTests
    {
        private ReportService _tested;

        [TestInitialize]
        public void Init()
        {
            RepoFactory.Instance = new Mocks.RepoFactory();
            _tested = new ReportService();
        }

        [TestMethod]
        public void ExpectedTotals()
        {
            var result = _tested.CustomerTotals(1, DateTime.MinValue, DateTime.MaxValue);
            Assert.AreEqual("Customer1", result[ReportConstants.Customer]);
            Assert.AreEqual("875", result[ReportConstants.AmountPerHour]);
            Assert.AreEqual("15,00", result[ReportConstants.NoOfHours]);
            // fixed price included twice but should only count once
            Assert.AreEqual(13125, double.Parse(result[ReportConstants.TotalAmount]));
        }

        [TestMethod]
        public void ExpectedWorkingHours()
        {
            var result = _tested.WorkingHours("1", DateTime.Today.AddDays(-3), DateTime.Today.AddDays(3));
            Assert.AreEqual("13,00", result[ReportConstants.TotalNoOfHours]);
            Assert.AreEqual("13,00", result[DateTime.Today.Year.ToString(CultureInfo.InvariantCulture)]);
        }

        [TestMethod]
        public void ExpectedFixedProjectSummary()
        {
            var result = _tested.ProjectSummary(2, DateTime.MinValue, DateTime.MaxValue);
            Assert.AreEqual("10,00", result[ReportConstants.TotalNoOfHours]);
            Assert.AreEqual(10000, double.Parse(result[ReportConstants.TotalAmount]));
            Assert.AreEqual(1000, double.Parse(result[ReportConstants.AmountPerHour]));
        }

        [TestMethod]
        public void ExpectedRunningProjectSummary()
        {
            var result = _tested.ProjectSummary(1, DateTime.MinValue, DateTime.MaxValue);
            Assert.AreEqual("5,00", result[ReportConstants.TotalNoOfHours]);
            Assert.AreEqual(3125, double.Parse(result[ReportConstants.TotalAmount]));
            Assert.AreEqual("625", result[ReportConstants.AmountPerHour]);
        }

    }
}
