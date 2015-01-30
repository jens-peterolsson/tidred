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
    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        public IEnumerable<ProjectData> GetProjects(int? coId)
        {
            if (!coId.HasValue)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CoId is required.");
                return null;
            }

            var repo = RepoFactory.Instance.CreateProjectRepo();

            var result = repo.GetAllProjects(coId.Value).Select(p => 
                new ProjectData { CustomerId = p.CustomerId, ProjectId = p.ProjectId,
                    Name = p.Name, CustomerName = p.Customer.Name, FixedPrice = GetFixedPrice(p)});

            return result;
        }

        private long GetFixedPrice(Project p)
        {
            var fixedProject = p.ProjectFixedPrice ?? new ProjectFixedPrice();
            return fixedProject.Price;
        }

        public void PostProject(ProjectData projectData)
        {
            var repo = RepoFactory.Instance.CreateProjectRepo();

            var project = new Project
            {
                ProjectId = projectData.ProjectId,
                Name = projectData.Name,
                CustomerId = projectData.CustomerId
            };

            if(!ValidateProject(project)) {
                return;
            }

            repo.SaveProject(project, projectData.FixedPrice);
        }

        private bool ValidateProject(Project project)
        {
            if (project.CustomerId <= 0)
            {
                ModelState.AddModelError("CustomerId", "CustomerId is required.");
            }

            if (string.IsNullOrEmpty(project.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }

            return ModelState.IsValid;
        }

    }
}
