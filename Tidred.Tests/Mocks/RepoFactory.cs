using Tidred.Repo;

namespace Tidred.Tests.Mocks
{
  public class RepoFactory : IRepoFactory
  {
    public ICustomerRepository CreateCustomerRepo()
    {
        return new CustomerRepo();
    }

    public IProjectRepository CreateProjectRepo()
    {
        return new ProjectRepo();
    }

    public ITimeRepository CreateTimeRepo()
    {
        return new TimeRepo();
    }

    public IUserRepository CreateUserRepo()
    {
        return new UserRepo();
    }


    public IProjectFixedPriceRepository CreateProjectFixedPriceRepo()
    {
        throw new System.NotImplementedException();
    }
  }
}
