using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tidred.Repo;
using Tidred.WebApp.Controllers.ApiData.Dto;

namespace Tidred.WebApp.Controllers.ApiData
{
    [Authorize]
    [RoutePrefix("api/TimeRecords")]
    public class TimeRecordsController : ApiController
    {
        public IEnumerable<Entry> Post([FromBody]EntryParams args)
        {
            var repo = RepoFactory.Instance.CreateTimeRepo();

            var result = repo.GetEntries(args.UserId, args.Start, args.End, args.CustomerId, args.ProjectId)
                          .OrderByDescending(entry => entry.Day)
                          .ThenByDescending(entry => entry.TimeEntryId)
                          .Select(t => new Entry
                          {
                              Id = t.TimeEntryId,
                              ProjectId = t.ProjectId,
                              ProjectName = GetProjectName(t.Project),
                              CustomerId = t.CustomerId,
                              CustomerName = t.Customer.Name,
                              Hours = t.Hours,
                              Comment = t.Comment,
                              Day = t.Day.ToShortDateString(),
                              PriceTypeId = t.PriceTypeId,
                              PriceTypeName = t.PriceType.Code,
                              Rate = t.Rate
                          })
                          .ToList();

            return result;
        }

        private string GetProjectName(Project project)
        {
            if (project == null)
            {
                return string.Empty;
            }
            return project.Name;
        }

        public void Put(TimeEntry entry)
        {
            var repo = RepoFactory.Instance.CreateTimeRepo();

            if (entry.CustomerId <= 0)
            {
                ModelState.AddModelError("CustomerId", "Customer is required.");
            }
            if (entry.PriceTypeId <= 0)
            {
                ModelState.AddModelError("PriceTypeId", "Price type is required.");
            }

            if (entry.Day == DateTime.MinValue)
            {
                ModelState.AddModelError("Day", "Day is required.");
            }

            if (!ModelState.IsValid)
            {
                return;
            }

            if (entry.TimeEntryId > 0)
            {
                repo.Update(entry);
                return;
            }

            repo.Create(entry);
        }


        [Route("PriceTypes")]
        public IEnumerable<PriceTypeData> GetPriceTypes(int? coId)
        {
            if (!coId.HasValue)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CoId is required.");
                return null;
            }

            var repo = RepoFactory.Instance.CreateTimeRepo();
            var result = repo.GetAllPriceTypes(coId.Value).Select(p =>
                new PriceTypeData
                {
                    CoId = coId.Value,
                    PriceTypeId = p.PriceTypeId,
                    Code = p.Code,
                    Name = p.Name
                });

            return result;

        }
    }
}
