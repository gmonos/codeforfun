using System;
using System.Configuration;
using System.Data.SqlClient;
using Serilog;
using Serilog.Filters;
using Zags.Logging;
using Zags.Logging.Serilog;
using Zags.OrganizationService.Database;

namespace Zags.OrganizationService.AcceptanceTests
{
    public class Bootstrap
    {
        public void InstallDatabase(string datasetSqlScript = null)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "Master";
            ExecuteNonQuery(builder.ConnectionString, $"CREATE DATABASE [{databaseName}];");

            DatabaseEngine.PerformUpgrade(connectionString);

            if (!string.IsNullOrEmpty(datasetSqlScript))
                ExecuteNonQuery(connectionString, datasetSqlScript);
        }

        private static void ExecuteNonQuery(string connectionString, string commandText)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    var schemaSql = commandText;
                    foreach (var sql in
                        schemaSql.Split(
                            new[] { "GO" },
                            StringSplitOptions.RemoveEmptyEntries))
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UninstallDatabase()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "Master";
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();

                var dropCmd = $@"
                    IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{databaseName}')
                    BEGIN
                        ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        DROP DATABASE [{databaseName}];
                    END";
                using (var cmd = new SqlCommand(dropCmd, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        public void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .Enrich.WithProperty("ServiceName", "OrganizationService")
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.Logger(c =>
                    c.Enrich.FromLogContext()
                    .Filter.ByIncludingOnly(Matching.FromSource("DomainTrakingEvent")))
                .CreateLogger();
            LogManager.Use<SerilogFactory>();
        }
    }
}
