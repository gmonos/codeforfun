using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zags.Utilities.Functional;

namespace Zags.DataAccess
{
    public static class PaginationUtilities
    {
        private const string LinkUrl = "?page={0}&PageSize={1}";

        public static bool IsPartialResult(PaginationInfo paginationInfo)
        {
            return paginationInfo.PageSize < paginationInfo.TotalCount;
        }

        public static string CheckPaginationInfo(PaginationInfo paginationInfo)
        {
            int maxPageSize;

            if (int.TryParse(ConfigurationManager.AppSettings["MaxPageSize"], out maxPageSize) && paginationInfo.PageSize > maxPageSize)
            {
                return string.Format("Page size can't be higher than : {0}", maxPageSize);
            }

            if (paginationInfo.OneBasedCurrentPage < 1)
            {
                return string.Format(string.Format("Page can't be lower than 1"));
            }

            if (paginationInfo.PageSize < 1)
            {
                return string.Format(string.Format("Page size can't be lower than 1"));
            }

            //if (!paginationInfo.IsCurrentPageNumberValid())
            //{
            //    return string.Format(string.Format("Requested page not allowed"));
            //}
            
            return string.Empty;
        }

        public static Tuple<string, string> GetLinks(string url, PaginationInfo paginationInfo, string otherParameters)
        {
            var links = new PaginatedLink();

            links.First = ConstructLink(url, string.Format(LinkUrl, 1, paginationInfo.PageSize), otherParameters);
            links.Prev = ConstructLink(url, string.Format(LinkUrl, paginationInfo.OneBasedCurrentPage - 1 <= 0 ? 1 : paginationInfo.OneBasedCurrentPage - 1, paginationInfo.PageSize), otherParameters);
            links.Next = ConstructLink(url, string.Format(LinkUrl, paginationInfo.OneBasedCurrentPage + 1 >= paginationInfo.TotalPages ? paginationInfo.OneBasedCurrentPage : paginationInfo.OneBasedCurrentPage + 1, paginationInfo.PageSize), otherParameters);
            links.Last = ConstructLink(url, string.Format(LinkUrl, paginationInfo.TotalPages, paginationInfo.PageSize), otherParameters);

            return new Tuple<string, string>("Link", JsonConvert.SerializeObject(links));
        }

        private static string ConstructLink(string url, string info, string otherParameters)
        {
            return string.Format("{0}{1}&{2}", url, info, otherParameters);
        }

        public static List<Tuple<string, string>> GetPaginatedHeaderInfo(PaginationInfo paginationInfo)
        {
            var value = new List<Tuple<string, string>>();
            value.Add(new Tuple<string, string>("X-Pagination-Per-Page", paginationInfo.PageSize.ToString()));
            value.Add(new Tuple<string, string>("X-Pagination-Current-Page", paginationInfo.OneBasedCurrentPage.ToString()));
            value.Add(new Tuple<string, string>("X-Pagination-Total-Pages", paginationInfo.TotalPages.ToString()));
            value.Add(new Tuple<string, string>("X-Pagination-Total-Entries", paginationInfo.TotalCount.ToString()));
            return value;
    }
    }
}
