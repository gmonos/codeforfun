using System.Collections.Generic;
using System.Linq;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Domain.Models
{

    public class OrganizationListModel
    {
        private Link _self;
        private IEnumerable<OrganizationModel> _items;

        public OrganizationListModel()
        {
 
        }

        public OrganizationListModel(IEnumerable<Organization> pms, string hostName)
        {
            _self = Link.Create(this, hostName);
            _items = pms.Select(pm => new OrganizationModel(pm, hostName));
        }

        public Link Self
        {
            get { return _self; }
            set { _self = value; }
        }

        public IEnumerable<OrganizationModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
