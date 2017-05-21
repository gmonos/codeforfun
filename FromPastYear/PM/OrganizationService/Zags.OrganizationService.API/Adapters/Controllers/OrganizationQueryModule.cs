using Nancy;
using Zags.OrganizationService.Application;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.ViewModelRetrievers;
using Zags.OrganizationService.Domain.Models;
using Zags.Utilities;
using Zags.Utilities.Functional;
using System.Linq;
using System;
//using System.Web.Http.Routing;

namespace Zags.OrganizationService.API.Adapters.Controllers
{
    public class OrganizationQueryModule : NancyModule
    {
        private readonly IOrganizationRepository _repository;
        private const int maxPageSize = 50;

        public OrganizationQueryModule(IOrganizationRepository repository)
        {
            //this.RequiresMSOwinAuthentication();

           


            _repository = repository;

            Get["/Organizations"] = parameters =>
            {
                var retriever = new OrganizationModelRetriever(Globals.HostName, _repository);
                var organizationListModel = retriever.RetrieveOrganizations();

                //var pageSize = (Request.Query["pageSize"] != null & Request.Query["pageSize"] < maxPageSize ? Request.Query["pageSize"] : maxPageSize);
                //var page = (Request.Query["page"] != null & Request.Query["page"] > 1 ? Request.Query["page"] : 1);
                //var totalCount = organizationListModel.Items.Count();
                //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                //var urlHelper = new UrlHelper();
                //var prevLink = page > 1 ? urlHelper.Link("/Organizations",
                //    new
                //    {
                //        page = page - 1,
                //        pageSize = pageSize

                //    }) : "";
                //var nextLink = page < totalPages ? urlHelper.Link("ExpensesForGroup",
                //    new
                //    {
                //        page = page + 1,
                //        pageSize = pageSize,
                //    }) : "";
                //var paginationHeader = new
                //{
                //    currentPage = page,
                //    pageSize = pageSize,
                //    totalCount = totalCount,
                //    totalPages = totalPages,
                //    previousPageLink = prevLink,
                //    nextPageLink = nextLink
                //};

                //Context.Response.Headers.Add("X-Pagination",
                //Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
                return organizationListModel;
            };

            Get["/Organizations/{id}"] = parameters =>
            {
                var retriever = new OrganizationModelRetriever(Globals.HostName, _repository);
                Either<Error, OrganizationModel> model = retriever.RetrieveOrganization(parameters.id);
                return model.Match<dynamic>(
                    Right: _ => { return _; },
                    Left: _ => { return HttpStatusCode.NotFound; });

            };


            Get["/Organizations/{pmId}/addresses"] = parameters =>
            {
                return HttpStatusCode.OK;
            };

            Get["/Organizations/{pmId}/addresses/{addressid}"] = parameters =>
            {
                return HttpStatusCode.OK;
            };

            Get["/Organizations/{pmId}/ibans"] = parameters =>
            {
                return HttpStatusCode.OK;
            };

            Get["/Organizations/{pmId}/ibans/{ibanid}"] = parameters =>
            {
                return HttpStatusCode.OK;
            };
            
        }
    }
}
