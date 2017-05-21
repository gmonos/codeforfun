using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zags.Domain.Entity;
using Zags.Domain.Repositories;
using Zags.Utility.Functional;


namespace Zags.DataAccess.EF
{
    public class GenericEFRepository<TEntity, TContext> : IRepository<TEntity>
         where TEntity : class, IEntity
         where TContext : DbContext
    {
       protected TContext Context { get; set; }
       protected readonly DbSet<TEntity> Dbset;

        public GenericEFRepository(TContext context)
        {
            Context = context;
            Dbset = context.Set<TEntity>();
        }

        virtual public TEntity Add(TEntity newEntity)
        {
            Dbset.Add(newEntity);
            Context.SaveChanges();
            return newEntity;
            
        }

        virtual public void Update(TEntity entity)
        {
            Dbset.Update(entity);

            Context.SaveChanges();
        }

        virtual public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;

            Dbset.Update(entity);

            Context.SaveChanges();
        }

        virtual public void Delete(int id)
        {
            TEntity entity = Dbset.AsQueryable().Where(x => x.Id == id).FirstOrDefault();

            this.Update(entity);

            Context.SaveChanges();

        }


        virtual public Option<TEntity> GetById(int id)
        {
            return GetDeepById(id);
        }

       


        virtual protected Option<TEntity> GetDeepById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Dbset.AsQueryable().Where(x => x.Id == id);

            foreach (var include in includes)
                query = query.Include(include);

            return query.FirstOrDefault();
        }

        virtual protected void Update<T>(T entity)
            where T : class, IEntity
        {
            Context.Update<T>(entity);
            Context.SaveChanges();
        }

        virtual protected void Delete<T>(T entity)
            where T : class, IEntity
        {
            entity.IsDeleted = true;

            this.Update<T>(entity);
            Context.SaveChanges();
        }
    }

    public class RetrieverBase : IRetriever
    {
        virtual  public IEnumerable<QueryResult> Search<QueryResult>(string procName, object parameters) where QueryResult : class
        {
            throw new NotImplementedException();
        }
    }


}
