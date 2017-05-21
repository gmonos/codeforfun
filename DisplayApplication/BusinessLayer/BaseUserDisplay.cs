using System.Collections.Generic;
using DisplayApplication.BusinessModel;
using DisplayApplication.Interfaces;

namespace DisplayApplication.BusinessLayer
{
    public abstract class BaseUserDisplay : IBaseUserDisplay
    {
        protected abstract void DisplayUser(User user);

        public virtual void DisplayUsers(IUserProvider userProvider)
        {
            IterateUsers(userProvider.GetUsers());
        }

        protected virtual void IterateUsers(IEnumerable<User> users)
        {
            foreach (var curUser in users)
            {
                if (IsUserToBeDisplayed(curUser))
                {
                    DisplayUser(curUser);
                }
            }
        }
        
        protected virtual bool IsUserToBeDisplayed(User user)
        {
            return true;             
        }


    }
}
