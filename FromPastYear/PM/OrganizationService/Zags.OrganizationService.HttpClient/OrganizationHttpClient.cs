using Newtonsoft.Json;
using Polly;
using System;
using System.Linq;
using System.Net.Http;
using Zags.OrganizationService.Domain.Models;
using Zags.Utilities;
using Zags.Utilities.Functional;
using Zags.Web.HttpClient;

namespace Zags.OrganizationService.HttpClient
{
    public class OrganizationHttpClient : IOrganizationHttpClient
    {
        private static Policy retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(new[]
                    {
                        TimeSpan.FromMilliseconds(50),
                        TimeSpan.FromMilliseconds(100),
                        TimeSpan.FromMilliseconds(150)
                    }
                );

        private static string organizationBaseUrl = "http://localhost:5000";

        public OrganizationListModel GetOrganizations(string accessToken, Guid correlationId)
        {
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
            if (correlationId == null) throw new ArgumentNullException(nameof(correlationId));

            return retryPolicy.Execute(() =>
            {
                
                var requestBuilder = new RequestBuilder();
                var response = requestBuilder.CreateRequest(new HttpMethod("GET"), organizationBaseUrl + "/organizations")
                    .WithAccessToken(accessToken)
                    .WithCorrelationToken(correlationId)
                    .Build()
                    .GetResponse();
                response.HttpResponseMessage.EnsureSuccessStatusCode();
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                };
                return JsonConvert.DeserializeObject<OrganizationListModel>(response.HttpResponseMessage.Content.ReadAsStringAsync().Result, settings);
            });
        }

        public Option<OrganizationModel> GetOrganization(int organisationId, string accessToken, Guid correlationId)
        {
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
            if (correlationId == null) throw new ArgumentNullException(nameof(correlationId));

            var requestBuilder = new RequestBuilder();
            var response = requestBuilder.CreateRequest(new HttpMethod("GET"), organizationBaseUrl + "/organizations/" + organisationId)
                .WithAccessToken(accessToken)
                .WithCorrelationToken(correlationId)
                .Build()
                .GetResponse();
            if (response.HttpResponseMessage.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                };
                return JsonConvert.DeserializeObject<OrganizationModel>(response.HttpResponseMessage.Content.ReadAsStringAsync().Result, settings);
            }
            return NoneType.Default;
        }

        public Either<Error, int> CreateOrganization(Organization organization, string accessToken, Guid correlationId)
        {
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));
            if (correlationId == null) throw new ArgumentNullException(nameof(correlationId));

            Response response = null;
            var requestBuilder = new RequestBuilder();
            var request = requestBuilder.CreateRequest(new HttpMethod("POST"), organizationBaseUrl + "/organizations")
                    .WithAccessToken(accessToken)
                    .WithCorrelationToken(correlationId)
                    .WithContent(organization)
                    .Build();

            retryPolicy.Execute(() =>
            {
                response = request.GetResponse();
            });

            if (response != null && response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var absoluteUri = response.HttpResponseMessage.Headers.Location.AbsoluteUri;
                if (!String.IsNullOrEmpty(absoluteUri))
                {
                    string[] tabstring = absoluteUri.Split(new string[] { "/organizations/" }, StringSplitOptions.RemoveEmptyEntries);
                    int organizationId;
                    if (int.TryParse(tabstring.Last(), out organizationId))
                        return organizationId;
                }
            }

            return F.Error("Organisation hasn't been created");
        }
    }

    public class Organization
    {
        public int Effectif { get; set; }
        public int OrganizationId { get; set; }
        public string RaisonSociale { get; set; }

        public string FormeJuridique { get; set; }

        public string Reference { get; set; }

        public string SIRET { get; set; }

        public string CodeNAF { get; set; }

        public string IdentifiantConventionCollective { get; set; }

    }
}
