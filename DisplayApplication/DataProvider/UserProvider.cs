using System.Collections.Generic;
using DisplayApplication.BusinessModel;
using DisplayApplication.Interfaces;

namespace DisplayApplication.DataProvider
{
    public class UserProvider : IUserProvider
    {
        public IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User { Age = 24 },
                new User (),
                new User {Name ="Invalid user" },
                new User {Name ="Minor", Age=12 },
                new User {Name = "Valid user", Age=21 }
            };
        }
    }
}
