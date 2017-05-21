using System.Collections.Generic;
using DisplayApplication.BusinessModel;

namespace DisplayApplication.Interfaces
{
    public interface IAdultUserDisplay : IBaseUserDisplay
    {
        /// <summary>
        /// This method must remove users without a name or without an age
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        IEnumerable<User> RemoveWrongUsers(IEnumerable<User> users);
    }
}
