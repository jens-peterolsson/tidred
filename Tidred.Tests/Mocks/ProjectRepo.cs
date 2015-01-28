using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tidred.Repo;

namespace Tidred.Tests.Mocks
{
    public class ProjectRepo : BaseRepoMock<Project>, IProjectRepository
    {
        public Project GetProject(long projectId)
        {
            if (projectId == 2)
            {
                return TestData.CreateFixedProject(1, projectId);
            }
            return TestData.CreateRunningProject(1, projectId);
        }

        public IEnumerable<Project> GetAllProjects(int coId)
        {
            var result = new List<Project>
        {
          TestData.CreateRunningProject(1, 1),
          TestData.CreateFixedProject(1, 2)
        };

            return result;
        }

        public IEnumerable<PriceType> GetAllPriceTypes(int coId)
        {
            throw new NotImplementedException();
        }
    }
}
