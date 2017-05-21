using System.Reflection;
using Xunit.Sdk;
using Zags.OrganizationService.AcceptanceTests;
using Zags.OrganizationService.AcceptanceTests.Properties;

namespace Zags.OrganizationService.AcceptanceTests
{ 
    public class UseDatabaseAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            new Bootstrap().InstallDatabase(DatasetsResource.OrganizationDataset);
            base.Before(methodUnderTest);
        }
        

        public override void After(MethodInfo methodUnderTest)
        {
            base.After(methodUnderTest);
            new Bootstrap().UninstallDatabase();
        }
    }
}