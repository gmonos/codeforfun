using System.Collections.Generic;

namespace Zags.DataAccess
{
    public class PaginatedResponse<T>
    {
        public PaginationInfo PaginationInfo { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<T> Items { get; set; }

        public SortingInfo SortingInfo { get; set; }
    }
}
