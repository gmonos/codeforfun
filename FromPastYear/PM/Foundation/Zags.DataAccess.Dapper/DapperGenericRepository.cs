
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Zags.Domain;
using Zags.Utilities.Functional;

namespace Zags.DataAccess.Dapper
{
    public class DapperGenericRepository<TEntity> : IDapperGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected string ConnectionString { get; set; } 

        public static R Using<TDisp, R>(TDisp disposable, Func<TDisp, R> func) where TDisp : IDisposable
        {
            using (disposable) return func(disposable);
        }

        public static R Connect<R>(string connStr, Func<IDbConnection, R> func)=> Using(new SqlConnection(connStr), conn => { conn.Open(); return func(conn); });

        public DapperGenericRepository()
        {
            ConnectionString  = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            Connect(ConnectionString, conn => conn.Insert(entity));
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            Connect(ConnectionString, conn => conn.Update(entity));
            return entity;
        }

        public virtual int Delete(int id)
        {
            return Connect(ConnectionString, conn => conn.Delete(id));
        }

        public virtual IEnumerable<TEntity> Search(object criteria)
        {
            return Connect(ConnectionString, conn => conn.GetList<TEntity>(criteria));
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return Connect(ConnectionString, conn => conn.GetList<TEntity>());
        }

        public virtual Option<TEntity> FindById(int id)
        {
            return Connect(ConnectionString, conn => conn.Get<TEntity>(id));
        }

        public virtual Option<TEntity> GetDeepById(string sql, int id, Type[] types, Func<object[], TEntity> map)
        {
            return Connect(ConnectionString, conn => conn.Query<TEntity>(types: types, sql: sql, map: map, param: new { Id = id }).SingleOrDefault());
        }

        public PaginatedResponse<TEntity> FindBy(PaginationInfo paginationInfo, SortingInfo sortingInfo, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
