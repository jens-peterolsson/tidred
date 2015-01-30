using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public User GetUser(string userId)
        {
            var user = Context.AspNetUsers.Single(u => u.Id == userId);
            var result = new User()
            {
                UserId = userId,
                UserName = user.UserName,
                CoId = user.Company.CoID
            };
            return result;
        }

        public string GetUserId(string userName)
        {
            var userId = Context.AspNetUsers.Single(u => u.UserName == userName).Id;

            return userId;
        }

        public IEnumerable<User> GetUsers(int coId)
        {
            var users = Context.AspNetUsers.Where(u => u.Company.CoID == coId)
                .Select(user => new User
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    CoId = user.Company.CoID
                });

            return users;
        }

        public void AddCompany(string userId, int coId)
        {
            var accountUser = Context.AspNetUsers.Single(u => u.Id == userId);
            accountUser.Company = Context.Companies.Single(c => c.CoID == coId);

            Context.SaveChanges();
        }

    }
}
