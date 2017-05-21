using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zags.DataAccess;
using Zags.Domain;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.API.Adapters.Controllers
{
    public static class QueryModuleUtilities<T> 
        where T : class, IEntity
    {
        public static Negotiator FindBy(Request request, Negotiator negotiate, IGenericRepository<T> _repository)
        {
            List<Tuple<string, string>> headers = new List<Tuple<string, string>>();
            PaginatedResponse<T> response;

            var paginationInfo = GetPaginationInfo(request);
            var reason = PaginationUtilities.CheckPaginationInfo(paginationInfo);
            if (!string.IsNullOrEmpty(reason))
            {
                return negotiate
                    .WithHeaders(headers.ToArray())
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithReasonPhrase(reason);
            }

            response = _repository.FindBy(paginationInfo, GetSortingInfo(request), null);

            if (response.Items == null && !response.PaginationInfo.IsCurrentPageNumberValid())
            {
                reason = "Requested page not allowed";
                return negotiate
                    .WithHeaders(headers.ToArray())
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithReasonPhrase(reason);
            }

            headers.Add(PaginationUtilities.GetLinks(request.Url.SiteBase + request.Url.Path, response.PaginationInfo, string.Format("Sort={0}", response.SortingInfo.Property)));
            headers.AddRange(PaginationUtilities.GetPaginatedHeaderInfo(response.PaginationInfo));

            if (!response.SortingInfo.IsValid)
            {
                return negotiate
                        .WithHeaders(headers.ToArray())
                        .WithStatusCode(HttpStatusCode.BadRequest)
                        .WithReasonPhrase(response.SortingInfo.Reason);
            }

            return negotiate
                    .WithModel(response.Items)
                    .WithHeaders(headers.ToArray())
                    .WithStatusCode(PaginationUtilities.IsPartialResult(response.PaginationInfo) ? HttpStatusCode.PartialContent : HttpStatusCode.OK);
        }

        private static SortingInfo GetSortingInfo(Request request)
        {
            SortingInfo sortingInfo = new SortingInfo();


            var sortProp = request.Query.Sort;
            if (!string.IsNullOrEmpty(sortProp))
            {
                sortingInfo.Property = sortProp;
            }

            return sortingInfo;
        }

        private static PaginationInfo GetPaginationInfo(Request request)
        {
            int pageSize;
            int page;

            int.TryParse(request.Query.PageSize, out pageSize);
            int.TryParse(request.Query.Page, out page);

            return new PaginationInfo(page, pageSize);
        }
    }
}
