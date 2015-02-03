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
        IEnumerable<User> GetUsers(int coId);
        void AddCompany(string userId, int coId);
        string GetUserId(string userName);
        UserTimeEntryPref GetUserPrefs(string userId);
        void StoreUserPrefs(UserTimeEntryPref prefs);
        IEnumerable<WorkingSchedule> GetWorkingSchedules(int coId);
    }
}
