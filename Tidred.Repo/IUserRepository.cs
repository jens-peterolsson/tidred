using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public interface IUserRepository
    {
        User GetUser(string userId);
    }
}
