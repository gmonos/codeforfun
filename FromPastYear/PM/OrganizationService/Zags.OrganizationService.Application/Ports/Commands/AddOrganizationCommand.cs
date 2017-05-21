using System;
using System.Collections.Generic;
using Zags.Application.Brighter.Command;
using Zags.Domain.Validation;
using Zags.Utilities;
using Zags.Utilities.Functional;

namespace Zags.OrganizationService.Application.Ports.Commands
{
    public class AddOrganizationCommand : CommandBase, ICanBeValidated<AddOrganizationCommand>
    {
        public AddOrganizationCommand() : base(Guid.NewGuid())
        {
        }

        public int Effectif { get; set; }
        public int OrganizationId { get; set; }
        public string RaisonSociale { get; set; }

        public string FormeJuridique { get; set; }

        public string Reference { get; set; }

        public string SIRET { get; set; }

        public string CodeNAF { get; set; }

        public string IdentifiantConventionCollective { get; set; }


        Either<List<Error>, AddOrganizationCommand> ICanBeValidated<AddOrganizationCommand>.Validate()
        {
            List<Error> errors = new List<Error>();
            if (Effectif < 0)
                errors.Add("Effectif cannot be negative");
            if (RaisonSociale == null || RaisonSociale == string.Empty)
                errors.Add("Raison social is mandatory");

            if (errors.Count > 0)
                return errors;

            return this;
        }
    }
}
