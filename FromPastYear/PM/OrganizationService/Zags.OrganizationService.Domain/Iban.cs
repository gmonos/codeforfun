
using System.ComponentModel.DataAnnotations.Schema;
using Zags.Domain;

namespace Zags.OrganizationService.Domain
{
    [Table("Organization].[Iban")]
    public class Iban : IEntity
    {
        public string Titulaire { get; set; }

        public string IBAN { get; set; }

        public string BIC { get; set; }

        public int Banque { get; set; }

        public int Id { get; set; }

        public IExtension Extension { get; set; }
    }
}
