using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Domain.Models
{
    public class Link
    {
        public Link(string relName, string href)
        {
            this.Rel = relName;
            this.HRef = href;
        }

        public Link()
        {
            //Required for serialiazation
        }

        public string Rel { get; set; }
        public string HRef { get; set; }

        public static Link Create(Organization pm, string hostName)
        {
            var link = new Link
            {
                Rel = "item",
                HRef = string.Format("http://{0}/{1}/{2}", hostName, "organization", pm.Id)
            };
            return link;
        }

        public static Link Create(OrganizationListModel organizationList, string hostName)
        {
            var self = new Link
            {
                Rel = "self",
                HRef = string.Format("http://{0}/{1}", hostName, "organization")
            };

            return self;
        }

        public override string ToString()
        {
            return string.Format("<link rel=\"{0}\" href=\"{1}\" />", Rel, HRef);
        }
    }
}
