namespace Tidred.Repo
{
    public interface IRepoFactory
    {
        IUserRepository CreateUserRepo();
        ICustomerRepository CreateCustomerRepo();
        IProjectRepository CreateProjectRepo();
        IProjectFixedPriceRepository CreateProjectFixedPriceRepo();
        ITimeRepository CreateTimeRepo();
    }
}
