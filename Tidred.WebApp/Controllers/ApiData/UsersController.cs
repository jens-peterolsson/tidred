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

        [Route("Prefs")]
        public UserPrefs GetUserPrefs(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UserId is required.");
                return null;
            }

            var repo = RepoFactory.Instance.CreateUserRepo();

            var prefs = repo.GetUserPrefs(userId);
            var result = new UserPrefs
            {
                CustomerId = prefs.CustomerId,
                PriceTypeId = prefs.PriceTypeId,
                ProjectId = prefs.ProjectId,
                UserId = prefs.UserId
            };

            return result;
        }

        [Route("Prefs")]
        public void PutUserPrefs(UserTimeEntryPref prefs)
        {
            var repo = RepoFactory.Instance.CreateUserRepo();
            repo.StoreUserPrefs(prefs);
        }

    }
}
