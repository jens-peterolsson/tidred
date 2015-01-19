namespace Tidred.Repo
{
    public class RepoFactory : IRepoFactory
    {
        private RepoFactory()
        {
        }

        private static IRepoFactory _instance;
        public static IRepoFactory Instance
        {
            get { return _instance ?? (_instance = new RepoFactory()); }
            set { _instance = value; }
        }

        public IUserRepository CreateUserRepo()
        {
            return new UserRepository();
        }

        public ICustomerRepository CreateCustomerRepo()
        {
            return new CustomerRepository();
        }

        public IProjectRepository CreateProjectRepo()
        {
            return new ProjectRepository();
        }

        public IProjectFixedPriceRepository CreateProjectFixedPriceRepo()
        {
            return new ProjectFixedPriceRepository();
        }

        public ITimeRepository CreateTimeRepo()
        {
            return new TimeRepository();
        }
    }
}
