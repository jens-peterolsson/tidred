using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly TidredContext _context = new TidredContext();

        public User GetUser(string userId)
        {
            var user = _context.AspNetUsers.Single(u => u.Id == userId);
            var result = new User()
            {
                UserId = userId,
                UserName = user.UserName,
                CoId = user.Company.CoID
            };
            return result;
        }

    }
}
