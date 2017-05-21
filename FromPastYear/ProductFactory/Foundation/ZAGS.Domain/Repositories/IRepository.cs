using System;
using System.Linq.Expressions;
using Zags.Domain.Entity;
using Zags.Utility.Functional;


namespace Zags.Domain.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Add(TEntity newEntity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);

        Option<TEntity> GetById(int id);

       
    }
}
