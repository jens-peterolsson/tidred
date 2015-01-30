using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public class ProjectFixedPriceRepository : BaseRepo<ProjectFixedPrice>, IProjectFixedPriceRepository
    {
        public IEnumerable<ProjectFixedPrice> GetProjectFixedPrices(int coId)
        {
            var projectFixedPrices = from projectFixedPrice in Context.ProjectFixedPrices
                                     join project in Context.Projects
                                     on projectFixedPrice.ProjectId equals project.ProjectId
                                     join customer in Context.Customers
                                     on project.CustomerId equals customer.CustomerId
                                     where customer.CoID == coId
                                     select projectFixedPrice;

            return projectFixedPrices;
        }

        public ProjectFixedPrice GetProjectFixedPrice(long projectId)
        {
            return Context.ProjectFixedPrices.SingleOrDefault(p => p.ProjectId == projectId);
        }

    }
}
