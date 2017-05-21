using System;
using DisplayApplication.BusinessModel;

namespace DisplayApplication.Helper
{
    public static class UserDisplayExtension
    {
        public static void Display(this User user)
        {
            Console.WriteLine("User : {0} - Age : {1}", user.Name, user.Age);
        }
    }
}
