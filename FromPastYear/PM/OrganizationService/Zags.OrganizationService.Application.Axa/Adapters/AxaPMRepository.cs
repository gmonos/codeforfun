using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Axa.Ports.Domain;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Application.Axa.Adapters
{

    public class AxaOrganizationRepository : OrganizationRepository
    {
        protected IDbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public override Organization Insert(Organization newPM)
        {
            base.Insert(newPM);

            using (IDbConnection db = _connection)
            {
                db.Open();
                var sqlQuery = "INSERT INTO Organization_Axa (OrganizationId, NumAbonne,CreationDate, ModificationDate) VALUES (@OrganizationId, @NumAbonne,GETDATE(),GETDATE());";
                db.Execute(sqlQuery, (OrganizationAxa)newPM.Extension);
            }


            return newPM;
        }
    }
}
