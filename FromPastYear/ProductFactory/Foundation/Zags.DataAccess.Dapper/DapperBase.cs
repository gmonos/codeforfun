using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Zags.DataAccess.Dapper
{
    public abstract class DapperBase
    {
        protected string ConnectionString { get; set; }
        
        public DapperBase(string connectionString)
        {
            ConnectionString = connectionString;
            SqlMapperExtensions.TableNameMapper = (type) => type.Name;
        }


        protected R Connect<R>(string connStr, Func<IDbConnection, R> func) => Using(CreateDbConnection(connStr), conn => { conn.Open(); return func(conn); });

        protected R Using<TDisp, R>(TDisp disposable, Func<TDisp, R> func) where TDisp : IDisposable
        {
            using (disposable) return func(disposable);
        }
        public virtual IDbConnection CreateDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }

}

