﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Zags.OrganizationService.AcceptanceTests.Specflow.ATTD
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class SearchAnOrganizationSimpleSearchFeature : Xunit.IClassFixture<SearchAnOrganizationSimpleSearchFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SearchAnOrganizationSimpleSearch.feature"
#line hidden
        
        public SearchAnOrganizationSimpleSearchFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SearchAnOrganizationSimpleSearch", "\tUS 190314\r\n\tEn tant que gestionnaire habilité.e,\r\n\tJe veux rechercher une person" +
                    "ne morale au sein du référentiel des personnes morales avec un nombre minimal de" +
                    " critères\r\n\tAfin de pouvoir identifier un souscripteur ou une entreprise assurée" +
                    "  ", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void SetFixture(SearchAnOrganizationSimpleSearchFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Searching an organization without criteria check that all the organizations are r" +
            "eturned")]
        [Xunit.TraitAttribute("FeatureTitle", "SearchAnOrganizationSimpleSearch")]
        [Xunit.TraitAttribute("Description", "Searching an organization without criteria check that all the organizations are r" +
            "eturned")]
        [Xunit.TraitAttribute("Category", "IntegrationTest,")]
        [Xunit.TraitAttribute("Category", "Organization")]
        public virtual void SearchingAnOrganizationWithoutCriteriaCheckThatAllTheOrganizationsAreReturned()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Searching an organization without criteria check that all the organizations are r" +
                    "eturned", new string[] {
                        "IntegrationTest,",
                        "Organization"});
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("There are 3 organizations", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.When("I search an organization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("The result should contains 3 organizations", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "SearchAnOrganizationSimpleSearch")]
        [Xunit.TraitAttribute("Description", "Searching an organization by the RaisonSociale and Reference check that matching " +
            "organization is returned")]
        [Xunit.TraitAttribute("Category", "IntegrationTest,")]
        [Xunit.TraitAttribute("Category", "Organization")]
        [Xunit.InlineDataAttribute("The First Test Company", "C37805E9-6D6B-49BF-B667-B74ABBC34D13", "The First Test Company", new string[0])]
        [Xunit.InlineDataAttribute("The First Test Company", "", "The First Test Company", new string[0])]
        [Xunit.InlineDataAttribute("", "C37805E9-6D6B-49BF-B667-B74ABBC34D13", "The First Test Company", new string[0])]
        [Xunit.InlineDataAttribute("The Second Test Company", "5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36", "The Second Test Company", new string[0])]
        public virtual void SearchingAnOrganizationByTheRaisonSocialeAndReferenceCheckThatMatchingOrganizationIsReturned(string raisonSociale, string reference, string result, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "IntegrationTest,",
                    "Organization"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Searching an organization by the RaisonSociale and Reference check that matching " +
                    "organization is returned", @__tags);
#line 14
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "RaisonSociale",
                        "Reference",
                        "Effectif",
                        "FormeJuridique"});
            table1.AddRow(new string[] {
                        "The First Test Company",
                        "C37805E9-6D6B-49BF-B667-B74ABBC34D13",
                        "50",
                        "SARL"});
            table1.AddRow(new string[] {
                        "The Second Test Company",
                        "5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36",
                        "30",
                        "SARL"});
            table1.AddRow(new string[] {
                        "The Third Test Company",
                        "6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36",
                        "60",
                        "SA"});
#line 15
 testRunner.Given("There are 3 organizations with this following data:", ((string)(null)), table1, "Given ");
#line 20
 testRunner.When(string.Format("I search an organization by the {0} and {1}", raisonSociale, reference), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.Then(string.Format("The result should be the organization {0}", result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "SearchAnOrganizationSimpleSearch")]
        [Xunit.TraitAttribute("Description", "Searching an organization by RaisonSociale and Reference check that matching seve" +
            "rals organizations are returned")]
        [Xunit.TraitAttribute("Category", "IntegrationTest,")]
        [Xunit.TraitAttribute("Category", "Organization")]
        [Xunit.InlineDataAttribute("The*", "", "3", new string[0])]
        [Xunit.InlineDataAttribute("*Test Company", "", "3", new string[0])]
        [Xunit.InlineDataAttribute("", "*F36", "2", new string[0])]
        [Xunit.InlineDataAttribute("", "*A8EE1A0-25BE-4E00*", "2", new string[0])]
        public virtual void SearchingAnOrganizationByRaisonSocialeAndReferenceCheckThatMatchingSeveralsOrganizationsAreReturned(string raisonSociale, string reference, string organizationCount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "IntegrationTest,",
                    "Organization"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Searching an organization by RaisonSociale and Reference check that matching seve" +
                    "rals organizations are returned", @__tags);
#line 31
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "RaisonSociale",
                        "Reference",
                        "Effectif",
                        "FormeJuridique"});
            table2.AddRow(new string[] {
                        "The First Test Company",
                        "C37805E9-6D6B-49BF-B667-B74ABBC34D13",
                        "50",
                        "SARL"});
            table2.AddRow(new string[] {
                        "The Second Test Company",
                        "5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36",
                        "30",
                        "SARL"});
            table2.AddRow(new string[] {
                        "The Third Test Company",
                        "6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36",
                        "60",
                        "SA"});
#line 32
 testRunner.Given("There are 3 organizations with this following data:", ((string)(null)), table2, "Given ");
#line 37
 testRunner.When(string.Format("I search an organization by the {0} and {1}", raisonSociale, reference), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 38
 testRunner.Then(string.Format("The result should be {0}", organizationCount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "SearchAnOrganizationSimpleSearch")]
        [Xunit.TraitAttribute("Description", "Searching an organization by RaisonSociale and Reference check that none organiza" +
            "tion are returned")]
        [Xunit.TraitAttribute("Category", "IntegrationTest,")]
        [Xunit.TraitAttribute("Category", "Organization")]
        [Xunit.InlineDataAttribute("The Fourth Test Company", "", new string[0])]
        [Xunit.InlineDataAttribute("", "12345678-25BE-4E00-BEAD-A9BF76988F36", new string[0])]
        public virtual void SearchingAnOrganizationByRaisonSocialeAndReferenceCheckThatNoneOrganizationAreReturned(string raisonSociale, string reference, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "IntegrationTest,",
                    "Organization"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Searching an organization by RaisonSociale and Reference check that none organiza" +
                    "tion are returned", @__tags);
#line 48
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "RaisonSociale",
                        "Reference",
                        "Effectif",
                        "FormeJuridique"});
            table3.AddRow(new string[] {
                        "The First Test Company",
                        "C37805E9-6D6B-49BF-B667-B74ABBC34D13",
                        "50",
                        "SARL"});
            table3.AddRow(new string[] {
                        "The Second Test Company",
                        "5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36",
                        "30",
                        "SARL"});
            table3.AddRow(new string[] {
                        "The Third Test Company",
                        "6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36",
                        "60",
                        "SA"});
#line 49
 testRunner.Given("There are 3 organizations with this following data:", ((string)(null)), table3, "Given ");
#line 54
 testRunner.When(string.Format("I search an organization by the {0} and {1}", raisonSociale, reference), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then("The result should be empty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                SearchAnOrganizationSimpleSearchFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                SearchAnOrganizationSimpleSearchFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
