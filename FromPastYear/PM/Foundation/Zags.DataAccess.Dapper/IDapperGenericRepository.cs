using System;
using System.Collections.Generic;
using Zags.Domain;
using Zags.Utilities.Functional;

namespace Zags.DataAccess.Dapper
{
    public interface IDapperGenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Search(object criteria);

        IEnumerable<TEntity> FindAll();

        Option<TEntity> GetDeepById(string sql, int id, Type[] types, Func<object[], TEntity> map);
    }
}
