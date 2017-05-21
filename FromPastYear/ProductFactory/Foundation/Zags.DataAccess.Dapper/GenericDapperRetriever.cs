using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Zags.Domain.Entity;
using Zags.Domain.Repositories;


namespace Zags.DataAccess.Dapper
{
    public class GenericDapperRetriever : DapperBase,IRetriever
    {
        public GenericDapperRetriever(string connectionString):base(connectionString)
        {
        }


        public virtual IEnumerable<T> Search<T>(string sql, object parameters) where T : class
        {
            CommandDefinition command = new CommandDefinition(commandText: sql, commandType: CommandType.Text, parameters: parameters);

            return Connect(ConnectionString, conn => conn.Query<T>(command));
        }


    }
}
