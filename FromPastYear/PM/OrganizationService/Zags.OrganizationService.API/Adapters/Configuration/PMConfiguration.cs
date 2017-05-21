using System;
using System.Configuration;

namespace Zags.OrganizationService.API.Adapters.Configuration
{
    public class OrganizationServerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("address")]
        public OrganizationUriSpecification Address
        {
            get { return this["address"] as OrganizationUriSpecification; }
            set { this["address"] = value; }
        }

        public static OrganizationServerConfiguration GetConfiguration()
        {
            var configuration =
                ConfigurationManager.GetSection("OrganizationServer") as OrganizationServerConfiguration;

            if (configuration != null)
                return configuration;

            return new OrganizationServerConfiguration();
        }
    }

    public class OrganizationUriSpecification : ConfigurationElement
    {
        [ConfigurationProperty("uri", DefaultValue = "http://localhost:5000/", IsRequired = true)]
        public Uri Uri
        {
            get { return (Uri)this["uri"]; }
            set { this["uri"] = value; }
        }
    }
}
