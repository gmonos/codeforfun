using System.Collections.Generic;
using DisplayApplication.BusinessModel;

namespace DisplayApplication.Interfaces
{
    public  interface IUserProvider
    {
         IEnumerable<User> GetUsers();
    }
}
