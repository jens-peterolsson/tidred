using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tidred.Repo;

namespace Tidred.Tests.Mocks
{
    public class UserRepo : BaseRepoMock<User>, IUserRepository
    {

        public User GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(int coId)
        {
            throw new NotImplementedException();
        }

        public void AddCompany(string userId, int coId)
        {
            throw new NotImplementedException();
        }

        public string GetUserId(string userName)
        {
            throw new NotImplementedException();
        }

        public UserTimeEntryPref GetUserPrefs(string userId)
        {
            throw new NotImplementedException();
        }

        public void StoreUserPrefs(UserTimeEntryPref prefs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkingSchedule> GetWorkingSchedules(int coId)
        {
            var list = new List<WorkingSchedule>
            {
                new WorkingSchedule { CoId = TestData.TestCoId, StartMonth = 1, EndMonth = 5, WorkingHours = 8.25m},
                new WorkingSchedule { CoId = TestData.TestCoId, StartMonth = 6, EndMonth = 8, WorkingHours = 7.25m},
                new WorkingSchedule { CoId = TestData.TestCoId, StartMonth = 9, EndMonth = 12, WorkingHours = 8.25m}
            };
            return list;
        }
    }
}
