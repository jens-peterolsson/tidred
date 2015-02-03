using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tidred.WebApp.Models
{
    public enum ReportType
    {
        Flex,
        CustomerTotal,
        CustomerPeriodTotal,
        ProjectSummary,
        WorkingHours
    }

    public class ReportModel
    {
        public string UserId { get; set; }
        public int CoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long? CustomerId { get; set; }
        public long? ProjectId { get; set; }
        public ReportType ReportType { get; set; }
        public SelectList Customers { get; set; }
        public SelectList Projects { get; set; }
        public Dictionary<string, string> Result { get; set; }
    }
}