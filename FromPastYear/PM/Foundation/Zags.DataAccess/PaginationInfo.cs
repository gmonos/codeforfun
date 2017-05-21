namespace Zags.DataAccess
{
    public class PaginationInfo
    {
        private int _currentPage;
        private int _pageSize;
        private int maxPageSize;
        private int _totalCount;
        private bool isValid;

        /// <summary>
        /// Current selected page One based
        /// </summary>
        public int OneBasedCurrentPage
        {
            get { return _currentPage + 1; }
        }
        /// <summary>
        /// Current selected page Zero based
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
        }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
        }
        /// <summary>
        /// Number of item to skip considering the currentpage & the page size
        /// </summary>
        public int ItemToSkip
        {
            get { return _currentPage * _pageSize; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        public int TotalPages
        {
            get
            {
                int number = 0;
                if (TotalCount % PageSize != 0)
                    number = 1;

                return (TotalCount / PageSize) + number;
            }
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public PaginationInfo(int page = 1, int pagesize = 10)
        {
            if (page == 0)
            {
                page = 1;
            }

            if (pagesize == 0)
            {
                pagesize = 10;
            }

            _currentPage = page - 1;
            _pageSize = pagesize;
        }

        public bool IsCurrentPageNumberValid()
        {
            return OneBasedCurrentPage <= TotalPages;
        }
    }
}
