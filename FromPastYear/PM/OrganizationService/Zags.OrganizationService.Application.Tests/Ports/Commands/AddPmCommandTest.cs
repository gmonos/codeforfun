using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Zags.Domain.Validation;
using Zags.OrganizationService.Application.Ports.Commands;

namespace Zags.OrganizationService.Application.Tests.Ports.Commands
{
    public class AddOrganizationCommandTest
    {
        public static IEnumerable<AddOrganizationCommand[]> ValidTestData
                => new AddOrganizationCommand[][] {
                new AddOrganizationCommand[] { new AddOrganizationCommand()
                {
                    OrganizationId = 42,
                    FormeJuridique = "Ma forme Juridique",
                    Reference = "Ma Reference",
                    SIRET = "Mon Siret",
                    CodeNAF =  "Mon Code Naf",
                    IdentifiantConventionCollective = "IdentifiantConventionCollective",
                    Effectif = 42,
                    RaisonSociale = "Ma raison sociale"
                } }};

        public static IEnumerable<AddOrganizationCommand[]> EffectifNonValide
        => new AddOrganizationCommand[][] {
                new AddOrganizationCommand[] { new AddOrganizationCommand()
                {
                    OrganizationId = 42,
                    FormeJuridique = "Ma forme Juridique",
                    Effectif = -1,
                    RaisonSociale = "Ma raison sociale"
                } }};

        [Theory, InlineData(42, "Ma forme Juridique", "Ma Reference", "Mon Siret", 
            "Mon Code Naf", "IdentifiantConventionCollective",42,"Ma raison sociale")]
        public void Validate_WhenValidCommandIsCreatedFromScratch_ValidationReturnsAValidCommad
            (int organizationId, string formeJuridique, string reference, string siret, string codeNAF
            ,string identifiantConventionCollective, int effectif, string raisonSociale)
        {
            var addPMCommand = new AddOrganizationCommand()
            {
                OrganizationId = organizationId,
                FormeJuridique = formeJuridique,
                Reference = reference,
                SIRET = siret,
                CodeNAF = codeNAF,
                IdentifiantConventionCollective = identifiantConventionCollective,
                Effectif = 42,
                RaisonSociale = raisonSociale

            };
            var validation = (addPMCommand as ICanBeValidated<AddOrganizationCommand>).Validate();

            addPMCommand.CodeNAF.Should().Be(codeNAF);
            addPMCommand.OrganizationId.Should().Be(organizationId);
            addPMCommand.FormeJuridique.Should().Be(formeJuridique);
            addPMCommand.Reference.Should().Be(reference);
            addPMCommand.SIRET.Should().Be(siret);
            addPMCommand.IdentifiantConventionCollective.Should().Be(identifiantConventionCollective);
            addPMCommand.Effectif.Should().Be(effectif);
            addPMCommand.RaisonSociale.Should().Be(raisonSociale);
            validation.IsLeft.Should().Be(false);
            validation.IsRight.Should().Be(true);
        }

        [Theory]
        [MemberData(nameof(ValidTestData))]
        public void Validate_WhenAddOrganizationCommandIsValid_ThenReturnedObjectIsAddOrganizationCommand(AddOrganizationCommand command)
        {
            var Validation = (command as ICanBeValidated<AddOrganizationCommand>).Validate();

            Validation.IsLeft.Should().Be(false);
            Validation.IsRight.Should().Be(true);
        }


        [Theory]
        [MemberData(nameof(EffectifNonValide))]
        public void Validate_WhenAddOrganizationCommandIsUnvalid_ThenReturnedObjectIsList(AddOrganizationCommand command)
        {
            var Validation = (command as ICanBeValidated<AddOrganizationCommand>).Validate();
            Validation.IsLeft.Should().Be(true);
            Validation.IsRight.Should().Be(false);
            //var Errors = Validation.Match(Right: x=>new Unit(), Left:(a)=> return a);


            
        }


   

    }
}
