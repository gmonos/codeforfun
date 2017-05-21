using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Domain.Models
{
    public class OrganizationModel
    {

        public OrganizationModel() { }

        public OrganizationModel(Organization pm, string hostName)
        {
            Id = pm.Id;
            RaisonSociale = pm.RaisonSociale;
            Effectif = pm.Effectif;
            Reference = pm.Reference;
            FormeJuridique = pm.FormeJuridique;
            CodeNAF = pm.CodeNAF;
            IdentifiantConventionCollective = pm.IdentifiantConventionCollective;
            SIRET = pm.SIRET;
            Self = new Link { Rel = "item", HRef = string.Format("http://{0}/{1}/{2}", hostName, "organizations", pm.Id) };
        }

        public int Id { get; set; }
        public string RaisonSociale { get; set; }
        public int? Effectif { get; set; }
        public string Reference { get; set; }
        public string CodeNAF { get; set; }
        public string FormeJuridique { get; set; }
        public string IdentifiantConventionCollective { get; set; }

        public string SIRET { get; set; }

        public Link Self { get; set; }
    }
}
