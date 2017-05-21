using DisplayApplication.Interfaces;
using System.Collections.Generic;
using DisplayApplication.BusinessModel;
using DisplayApplication.Helper;

namespace DisplayApplication.BusinessLayer
{
    public class AdultUserDisplay : BaseUserDisplay, IAdultUserDisplay
    {
        public override void DisplayUsers(IUserProvider userProvider)
        {
            var users = RemoveWrongUsers(userProvider.GetUsers());
            IterateUsers(users);
        }

        public IEnumerable<User> RemoveWrongUsers(IEnumerable<User> users)
        {
            List<User> goodUsers = new List<User>();

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Name) && user.Age.HasValue)
                    goodUsers.Add(user);
            }

            return goodUsers;
        }

        protected override void DisplayUser(User user)
        {
            user.Display();
        }

        protected override bool IsUserToBeDisplayed(User user)
        {
            return user.Age > 18;
        }
    }
}
