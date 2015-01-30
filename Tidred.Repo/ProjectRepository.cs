using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public class ProjectRepository : BaseRepo<Project>, IProjectRepository
    {
        public IEnumerable<Project> GetAllProjects(int coId)
        {
            var projects = (from project in Context.Projects
                           join customer in Context.Customers
                           on project.CustomerId equals customer.CustomerId
                           where customer.CoID == coId
                           select project);

            var test = projects.ToList();

            return projects;
        }

        public Project GetProject(long projectId)
        {
            return Context.Projects.SingleOrDefault(p => p.ProjectId == projectId); ;
        }

        public void SaveProject(Project projectArg, long fixedPrice)
        {
            var project = MigrateExisting(projectArg);
            SetFixedPrice(project, fixedPrice);

            if (project.ProjectId > 0)
            {
                Update(project);
            }
            else
            {
                Create(project);
            }
        }

        private Project MigrateExisting(Project project)
        {
            if (project.ProjectId == 0) return project;

            var existing = GetProject(project.ProjectId);
            Context.Entry(existing).CurrentValues.SetValues(project);

            return existing;
        }
        
        private void SetFixedPrice(Project project, long fixedPrice)
        {
            if (fixedPrice <= 0) return;

            if (project.ProjectFixedPrice == null)
            {
                project.ProjectFixedPrice = new ProjectFixedPrice { ProjectId = project.ProjectId};
            }

            project.ProjectFixedPrice.Price = fixedPrice;
        }

    }
}
