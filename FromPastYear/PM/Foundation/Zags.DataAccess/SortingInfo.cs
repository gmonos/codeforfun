namespace Zags.DataAccess
{
    public class SortingInfo
    {
        public bool IsValid = true;
        public string Reason;
        private string _property;
        public string Property
        {
            get { return _property; }
            set
            {
                if (value.StartsWith("-"))
                {
                    _order = false;
                    _property = value.Substring(1);
                }
                else
                {
                    _property = value;
                }

                if (string.IsNullOrEmpty(_property))
                {
                    IsValid = false;
                    Reason = "Sorting information is invalid";
                }
            }
        }

        private bool _order;
        /// <summary>
        /// 0 => Ascending
        /// 1 => Descending
        /// </summary>
        public bool Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public SortingInfo(string prop = "Id", bool order = true)
        {
            if (prop.StartsWith("-"))
            {
                order = false;
                prop = prop.Substring(1);
            }

            _property = prop;
            _order = order;
        }
    }
}
