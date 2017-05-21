using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zags.OrganizationService.HttpClient;

namespace Zags.IdentityServer.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var token = (User.Identity as ClaimsIdentity).FindFirst("access_token");
            var organizationHttpClient = new OrganizationHttpClient();
            var organizationList = organizationHttpClient.GetOrganizations(token.Value, Guid.NewGuid());

            return View();
        }
        private void GetOrganizations()
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            var token = (User.Identity as ClaimsIdentity).FindFirst("access_token");
            var organizationHttpClient = new OrganizationHttpClient();
            for (int i = 3000; i < 103000; i++)
            {
                var organizationList = organizationHttpClient.GetOrganization(i, token.Value, Guid.NewGuid());
            }
            st.Stop();
            long time = st.ElapsedMilliseconds;
            Debug.Write("READ : " + time);
        }

        private void CreateOrganizations()
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            var token = (User.Identity as ClaimsIdentity).FindFirst("access_token");
            var organizationHttpClient = new OrganizationHttpClient();
            var organization = new Organization
            {
                RaisonSociale = "Raison Sociale CMA from HttpClient",
                Effectif = 34,
                FormeJuridique = "Forme Jruisdique 2",
                SIRET = "123456789012398",
                CodeNAF = "12345",
                Reference = "000459",
                IdentifiantConventionCollective = "0878234"
            };
            for (int i = 0; i < 4000; i++)
            {
                organizationHttpClient.CreateOrganization(organization, token.Value, Guid.NewGuid());
            }
            st.Stop();
            long time = st.ElapsedMilliseconds;
            Debug.Write("Write : " + time);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var token = (User.Identity as ClaimsIdentity).FindFirst("access_token");
            var organizationHttpClient = new OrganizationHttpClient();
            var organizationList = organizationHttpClient.GetOrganizations(token.Value, Guid.NewGuid());
            var organization = new Organization
            {
                RaisonSociale = "Raison Sociale CMA from HttpClient",
                Effectif = 34,
                FormeJuridique = "Forme Jruisdique 2",
                SIRET = "123456789012398",
                CodeNAF = "12345",
                Reference = "000459",
                IdentifiantConventionCollective = "0878234"
            };
            Stopwatch st = new Stopwatch();
            st.Start();
            Parallel.Invoke(() => CreateOrganizations(), () => GetOrganizations());
            st.Stop();
            long time = st.ElapsedMilliseconds;
            Debug.Write(time);

            return View();
        }
    }
}