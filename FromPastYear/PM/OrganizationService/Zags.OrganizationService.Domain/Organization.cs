using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zags.Domain;
using Zags.OrganizationService.Domain.Events;

namespace Zags.OrganizationService.Domain
{
    [Table("Organization].[Organization")]
    public class Organization : AggregateRoot<Organization>,IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Reference { get; private set; }
        public string RaisonSociale { get; private set; }
        public string FormeJuridique { get; private set; }
        public int? Effectif { get; private set; }
        public string SIRET { get; private set; }
        public string CodeNAF { get; private set; }
        public string IdentifiantConventionCollective { get; private set; }
        public IExtension Extension
        {
            get; set;
        }

        public List<Address> Addresses { get;  set; }

        public List<Iban> Ibans { get; set; }

        #region Properties removed from the old model
        /// <summary>
        /// 
        /// </summary>
        public string Activite { get; set; }


        public string Telephone1 { get; set; }

        public string Telephone2 { get; set; }

        public string NumeroFax { get; set; }

        public string Email { get; set; }

        public string SiteWeb { get; set; }


        #endregion

        public Organization()
        {
        }

        public Organization(
            int pmId,
            string reference, 
            string raisonSociale,
            string formeJuridique = null,
            int? effectif = null,
            string siret = null,
            string naf = null, 
            string identifiantConventionCollective = null)
        {
            AggregateId = Guid.NewGuid();
            Id = pmId;
            FormeJuridique = formeJuridique;
            RaisonSociale = raisonSociale;
            Reference = reference;
            Effectif = effectif;
            SIRET = siret;
            CodeNAF = naf;
            IdentifiantConventionCollective = identifiantConventionCollective;
            Addresses = new List<Address>();
            Ibans = new List<Iban>();
        }

        /// <summary>
        /// This method is used to change the internal state of the object 
        /// An Event tracking all the changes is store in the "changes" collection of the Aggregate root base class 
        /// </summary>
        public void ChangeValues(
            string raisonSociale,
            string formeJuridique = null,
            int? effectif = null,
            string siret = null,
            string naf = null,
            string identifiantConventionCollective = null)
        {
            var pmChangedValuesEvent = new OrganizationChangedValuesEvent<Organization>();
            pmChangedValuesEvent.AsOf = DateTime.Now;
            pmChangedValuesEvent.AggregateId = AggregateId;
            pmChangedValuesEvent.EventId = Guid.NewGuid();
            pmChangedValuesEvent.OldValue = new Organization(Id,null,RaisonSociale, FormeJuridique, Effectif, SIRET, CodeNAF, IdentifiantConventionCollective); 
            pmChangedValuesEvent.OrganizationId = Id;
            pmChangedValuesEvent.NewValue = new Organization(Id, null, raisonSociale, formeJuridique, effectif, siret, naf, identifiantConventionCollective);

            RaisonSociale = raisonSociale;
            FormeJuridique = formeJuridique;
            Effectif = effectif;
            SIRET = siret;
            CodeNAF = naf;
            IdentifiantConventionCollective = identifiantConventionCollective;

            ApplyTrackingChange(pmChangedValuesEvent);

        }
    }
}
