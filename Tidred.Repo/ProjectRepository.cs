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
        private readonly TidredContext _context = new TidredContext();

        public IEnumerable<PriceType> GetAllPriceTypes(int coId)
        {
            return _context.PriceTypes.Where(p => p.CoID == coId);
        }

        public IEnumerable<Project> GetAllProjects(int coId)
        {
            var projects = from project in _context.Projects
                           join customer in _context.Customers
                           on project.CustomerId equals customer.CustomerId
                           where customer.CoID == coId
                           select project;

            return projects;
        }

        public Project GetProject(long projectId)
        {
            return _context.Projects.SingleOrDefault(p => p.ProjectId == projectId);
        }

    }
}
