
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zags.Domain;
using Zags.OrganizationService.Domain.Events;

namespace Zags.OrganizationService.Domain
{
    [Table("Organization].[Address")]
    public class Address : AggregateRoot<Address>, IEntity
    {
        [Key]
        public int Id { get; set; }

        public int CategorieAdresse { get; set; }

        public string NumVoie { get; set; }

        public string NomVoie { get; set; }

        public string ComplementVoie { get; set; }

        public string CodePostal { get; set; }

        public int Ville { get; set; }

        public int Pays { get; set; }

        #region Properties removed from the old model
        public int TypeVoie { get; set; }

        public IExtension Extension
        {
            get; set;
        }
        #endregion

        public Address()
        {
        }

        public Address(
            int id,
            int ville,
            int pays,
            int typeVoie,
            int categorieAdresse,
            string numVoie,
            string nomVoie = null,
            string complementVoie = null)
        {
            AggregateId = Guid.NewGuid();
            Id = id;
            Ville = ville;
            Pays = pays;
            TypeVoie = typeVoie;
            CategorieAdresse = categorieAdresse;
            NumVoie = numVoie;
            NomVoie = nomVoie;
            ComplementVoie = complementVoie;
        }

        /// <summary>
        /// This method is used to change the internal state of the object 
        /// An Event tracking all the changes is store in the "changes" collection of the Aggregate root base class 
        /// </summary>
        public void ChangeValues(
            int ville,
            int pays,
            int typeVoie,
            int categorieAdresse,
            string numVoie,
            string nomVoie = null,
            string complementVoie = null)
        {
            var pmChangedValuesEvent = new OrganizationChangedValuesEvent<Address>();
            pmChangedValuesEvent.AsOf = DateTime.Now;
            pmChangedValuesEvent.AggregateId = AggregateId;
            pmChangedValuesEvent.EventId = Guid.NewGuid();
            pmChangedValuesEvent.OldValue = new Address(Id, Ville, Pays, TypeVoie, CategorieAdresse, NumVoie, NomVoie, ComplementVoie);
            pmChangedValuesEvent.OrganizationId = Id;
            pmChangedValuesEvent.NewValue = new Address(Id, ville, pays, typeVoie, categorieAdresse, numVoie, nomVoie, complementVoie);

            Ville = ville;
            Pays = pays;
            TypeVoie = typeVoie;
            CategorieAdresse = categorieAdresse;
            NumVoie = numVoie;
            NomVoie = nomVoie;
            ComplementVoie = complementVoie;

            ApplyTrackingChange(pmChangedValuesEvent);

        }
    }
}
