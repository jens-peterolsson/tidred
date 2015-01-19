using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IUserRepo CreateUserRepo()
        {
            return new UserRepo();
        }
    }
}
