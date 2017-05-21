using DisplayApplication.BusinessLayer;
using DisplayApplication.DataProvider;
using DisplayApplication.Interfaces;
using System;

namespace DisplayApplication
{
    internal class Program
    {

        /*

             TODO:
        
            1 - In folder "BusinessLayer", implement a class displaying only users being 18 years old or more. Only users having a name AND an age have to be displayed
            Instance of this class must be provided by property "AdultUserDisplayProvider" below
            To display users, use method "Display" from class "Helper/UserDisplayExtension"     
            Your code should be testable as much as possible
            
            2 - Add tests in project "DisplayApplication.Tests" to test your new class       

            */

        /// <summary>
        /// Change implementation to use your provider
        /// </summary>
        protected static IAdultUserDisplay AdultUserDisplayProvider { get { return new AdultUserDisplay(); } }

        /// <summary>
        /// Do not change anything on Main
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            AdultUserDisplayProvider.DisplayUsers(new UserProvider());
            Console.ReadLine();
        }
     
    }
}