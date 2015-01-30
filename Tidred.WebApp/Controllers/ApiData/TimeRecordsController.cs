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
