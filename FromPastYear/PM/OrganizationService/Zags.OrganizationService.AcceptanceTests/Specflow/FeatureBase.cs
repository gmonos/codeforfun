using System.Configuration;
using System.Dynamic;
using Serilog;
using Serilog.Filters;
using TechTalk.SpecFlow;
using Zags.Logging;
using Zags.Logging.Serilog;
using Zags.OrganizationService.AcceptanceTests.Properties;

namespace Zags.OrganizationService.AcceptanceTests.Specflow
{
    [Binding]
    public class FeatureBase
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            new Bootstrap().InstallDatabase(DatasetsResource.OrganizationDataset);
            new Bootstrap().InitializeLogger();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
           new Bootstrap().UninstallDatabase();
        }
        
    }
}
