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
    public partial class SelectAnOrganizationFeature : Xunit.IClassFixture<SelectAnOrganizationFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SelectAnOrganization.feature"
#line hidden
        
        public SelectAnOrganizationFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SelectAnOrganization", "\tUS 190316\r\n\tEn tant que gestionnaire habilité.e,\r\n\tJe veux désigner la PM qui ré" +
                    "sulte de la recherche réalisée au sein du référentiel des personnes morales\r\n\tAf" +
                    "in de désigner le souscripteur d’un contrat cadre et l’entreprise assurée d’un c" +
                    "ontrat groupe", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        public virtual void SetFixture(SelectAnOrganizationFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "SelectAnOrganization")]
        [Xunit.TraitAttribute("Description", "Selecting an organization from the organization search list check that the select" +
            "ed organization details are showed")]
        [Xunit.TraitAttribute("Category", "IntegrationTest,")]
        [Xunit.TraitAttribute("Category", "Organization")]
        [Xunit.InlineDataAttribute("The First Test Company", "C37805E9-6D6B-49BF-B667-B74ABBC34D13", "50", "SARL", new string[0])]
        public virtual void SelectingAnOrganizationFromTheOrganizationSearchListCheckThatTheSelectedOrganizationDetailsAreShowed(string raisonSociale, string reference, string effectif, string formeJuridique, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "IntegrationTest,",
                    "Organization"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Selecting an organization from the organization search list check that the select" +
                    "ed organization details are showed", @__tags);
#line 8
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
#line 9
 testRunner.Given("I have this following organizations in the search list:", ((string)(null)), table1, "Given ");
#line 14
 testRunner.When(string.Format("I select the organization {0}", raisonSociale), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then(string.Format("the organization {0} with these {1} and {2} and {3} details should be displayed o" +
                        "n the screen", raisonSociale, reference, effectif, formeJuridique), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                SelectAnOrganizationFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                SelectAnOrganizationFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion