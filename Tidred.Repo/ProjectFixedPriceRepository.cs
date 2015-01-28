using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public class ProjectFixedPriceRepository : BaseRepo<ProjectFixedPrice>, IProjectFixedPriceRepository
    {
        private readonly TidredContext _context = new TidredContext();

        public IEnumerable<ProjectFixedPrice> GetProjectFixedPrices(int coId)
        {
            var projectFixedPrices = from projectFixedPrice in _context.ProjectFixedPrices
                                     join project in _context.Projects
                                     on projectFixedPrice.ProjectId equals project.ProjectId
                                     join customer in _context.Customers
                                     on project.CustomerId equals customer.CustomerId
                                     where customer.CoID == coId
                                     select projectFixedPrice;

            return projectFixedPrices;
        }

        public ProjectFixedPrice GetProjectFixedPriceWithoutTracking(long projectId)
        {
            return _context.ProjectFixedPrices.AsNoTracking().SingleOrDefault(p => p.ProjectId == projectId);
        }

    }
}
