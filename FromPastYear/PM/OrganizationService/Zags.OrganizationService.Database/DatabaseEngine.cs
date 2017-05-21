using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbUp;

namespace Zags.OrganizationService.Database
{
    public static class DatabaseEngine
    {
        public static DbUp.Engine.DatabaseUpgradeResult PerformUpgrade(string connectionString)
        {
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            return upgrader.PerformUpgrade();
        }
    }
}
