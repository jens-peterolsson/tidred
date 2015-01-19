using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tidred.Services;
using Tidred.WebApp.Models;

namespace Tidred.WebApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: /Report/
        public ActionResult Index()
        {
            var model = InitModel();
            return View("Result", model);
        }

        [HttpPost]
        public ActionResult ProcessSelection(ReportModel model)
        {
            model = InitModel(model);
            return Result(model);
        }

        private ActionResult Result(ReportModel model)
        {
            var reportService = ServiceFactory.Instance.CreateReportService();

            if (model == null)
            {
                return View("Result");
            }

            switch (model.ReportType)
            {
                case ReportType.CustomerTotal:
                    GetCustomerTotal(model, reportService);
                    break;
                case ReportType.WorkingHours:
                    model.Result = reportService.WorkingHours(Param.CurrentUserId, model.StartDate, model.EndDate);
                    break;
                case ReportType.ProjectSummary:
                    GetProjectSummary(model, reportService);
                    break;
            }

            return View("Result", model);
        }

        private void GetCustomerTotal(ReportModel model, IReportService reportService)
        {
            if (model.CustomerId.HasValue)
            {
                model.Result = reportService.CustomerTotals(model.CustomerId.Value, model.StartDate, model.EndDate);
            }
            else
            {
                model.Result.Add("Ingen kund angiven.", string.Empty);
            }
        }

        private void GetProjectSummary(ReportModel model, IReportService reportService)
        {
            if (model.ProjectId.HasValue)
            {
                model.Result = reportService.ProjectSummary(model.ProjectId.Value, model.StartDate, model.EndDate);
            }
            else
            {
                model.Result.Add("Inget projekt angivet.", string.Empty);
            }
        }

        private ReportModel InitModel(ReportModel inputModel = null)
        {
            var model = inputModel ?? new ReportModel();

            model.Result = new Dictionary<string, string>
            {
                { "Inget resultat inläst.", string.Empty }
            };

            var projectService = ServiceFactory.Instance.CreateProjectService();
            var customerService = ServiceFactory.Instance.CreateCustomerService();

            var customers = customerService.GetCustomers(Param.CurrentCoId).ToList();
            model.Customers = new SelectList(customers, "CustomerId", "Name");
            var projects = projectService.GetAllProjects(Param.CurrentCoId).ToList();
            model.Projects = new SelectList(projects, "ProjectId", "Name");

            if (model.EndDate == DateTime.MinValue)
            {
                model.EndDate = DateTime.MaxValue;
            }

            return model;
        }
    }
}