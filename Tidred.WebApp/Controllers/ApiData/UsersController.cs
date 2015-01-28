using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tidred.Repo;

namespace Tidred.WebApp.Controllers.ApiData
{
    [Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        public IEnumerable<User> GetUsers(int? coId)
        {
            if (!coId.HasValue)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CoId is required.");
                return null;
            }

            var repo = RepoFactory.Instance.CreateUserRepo();

            var result = repo.GetUsers(coId.Value);
            return result;
        }


    }
}
