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

        public UserTimeEntryPref GetUserPrefs(string userId)
        {
            return Context.UserTimeEntryPrefs.SingleOrDefault(u => u.UserId == userId);
        }

        public void StoreUserPrefs(UserTimeEntryPref prefs)
        {
            var hasExistingPrefs = (GetUserPrefs(prefs.UserId) != null);

            if (hasExistingPrefs)
            {
                UpdateUserPrefs(prefs);
            }
            else
            {
                CreateUserPrefs(prefs);
            }
        }

        private void CreateUserPrefs(UserTimeEntryPref prefs)
        {
            Context.UserTimeEntryPrefs.Add(prefs);
            Context.SaveChanges();
        }

        private void UpdateUserPrefs(UserTimeEntryPref prefs)
        {
            var existingPrefs = Context.UserTimeEntryPrefs.First(pref => pref.UserId == prefs.UserId);
            Context.Entry(existingPrefs).CurrentValues.SetValues(prefs);
            Context.SaveChanges();
        }

        public IEnumerable<WorkingSchedule> GetWorkingSchedules(int coId)
        {
            var result = Context.WorkingSchedules.Where(s => s.CoId == coId);
            return result;
        }
    }
}
