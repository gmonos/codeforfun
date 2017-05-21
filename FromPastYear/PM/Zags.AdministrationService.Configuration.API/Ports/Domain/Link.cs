namespace Zags.AdministrationService.Configuration.API.Ports.Domain
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

        public static Link Create(string hostName)
        {
            var link = new Link
            {
                Rel = "item",
                HRef = string.Format("http://{0}/{1}/{2}", hostName, "organization", 0)
            };
            return link;
        }

        public override string ToString()
        {
            return string.Format("<link rel=\"{0}\" href=\"{1}\" />", Rel, HRef);
        }
    }
}