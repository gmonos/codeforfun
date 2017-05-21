using System;
using System.Data;
using Dapper;
using Zags.DataAccess.Dapper;
using Zags.OrganizationService.Domain;
using Zags.Utilities.Functional;
using System.Linq;

namespace Zags.OrganizationService.Application.Adapters.DataAccess
{
    public class OrganizationRepository : DapperGenericRepository<Organization>, IOrganizationRepository
    {

        public OrganizationRepository() : base()
        {

        }

        //public override Option<Organization> GetById(int id)
        //{
        //    var sql = "SELECT vOrganization.* FROM vOrganization where vOrganization.ID=@Id";
        //    var organization = Connect(ConnectionString, conn => conn.Query<Organization>(sql, new { Id = id })).FirstOrDefault();
        //    return organization;
        //}

        public virtual Option<Organization> GetDeepById(int id)
        {
            var sql = "SELECT Organization.*, Address.*, Iban.* FROM Organization LEFT JOIN Address ON Organization.ID = Address.Organization_Id LEFT JOIN Iban ON Organization.ID = Iban.Organization_Id WHERE   Organization.Id= @Id";

            var types = new Type[] { typeof(Organization), typeof(Address), typeof(Iban) };

            var resut = base.GetDeepById(sql, id, types, DoMap);

            return resut;
        }


        protected virtual Organization DoMap(object[] r)
        {
            var pm = (r[0] as Organization);

            if(pm == null) return null;

            var address = r[1] as Address;

            if(address!=null) pm.Addresses.Add((Address)r[1]);

            var iban = r[2] as Iban;

            if (iban != null) pm.Ibans.Add((Iban)r[2]);

            return pm;
        }
    }
    
}
