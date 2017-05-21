using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zags.Domain;
using Zags.Utilities.Functional;

namespace Zags.DataAccess.EF
{
    public class EFGenericRepository<TEntity> : IEFGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        internal DbContext _context;
        internal IDbSet<TEntity> _dbSet;

        public EFGenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public PaginatedResponse<TEntity> FindBy(PaginationInfo paginationInfo, SortingInfo sortingInfo, Expression<Func<TEntity, bool>> predicate)
        {
            PaginatedResponse<TEntity> response = new PaginatedResponse<TEntity>();
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            queryable = PaginatedQueryable(paginationInfo, sortingInfo, queryable);

            response.PaginationInfo = paginationInfo;
            response.SortingInfo = sortingInfo;
            response.Items = queryable != null ? queryable.ToList() : null;

            return response;

        }

        private IQueryable<TEntity> PaginatedQueryable(PaginationInfo paginationInfo, SortingInfo sortingInfo, IQueryable<TEntity> queryable)
        {
            paginationInfo.TotalCount = queryable.Count();

            if (!paginationInfo.IsCurrentPageNumberValid())
            {
                return null;
            }

            if (sortingInfo.Property.ToLowerInvariant() == "id")
            {
                var lambda = Utilities.BuildLambdaForSorting<TEntity, int>(typeof(TEntity), sortingInfo.Property);

                if (lambda.IsSome)
                {
                    queryable = GetOrderedQuery(lambda, queryable, sortingInfo);
                    if (queryable == null)
                    {
                        return null;
                    }
                }
                else if (CheckSorting(sortingInfo, lambda) == null)
                {
                    return null;
                }

                var lambda = Utilities.BuildLambdaForSorting<TEntity, int>(typeof(TEntity), sortingInfo.Property).Match(
                    Some: x =>
                    {
                        return x;
                    },
                    None: () => { return null; });



            }
            else
            {
                var lambda = Utilities.BuildLambdaForSorting<TEntity, string>(typeof(TEntity), sortingInfo.Property).Match(
                    Some: x =>
                    {
                        return x;
                    },
                    None: () => { return null; });


                if (CheckSorting(sortingInfo, lambda) == null)
                {
                    return null;
                }

                queryable = GetOrderedQuery(lambda, queryable, sortingInfo);
                if (queryable == null)
                {
                    return null;
                }
            }

            return queryable.Skip(paginationInfo.ItemToSkip).Take(paginationInfo.PageSize);
        }

        private static Expression<Func<TEntity, T>> CheckSorting<T>(SortingInfo sortingInfo, Expression<Func<TEntity, T>> x)
        {
            if (x == null)
            {
                sortingInfo.IsValid = false;
                sortingInfo.Reason = string.Format("Incorrect sorting field {0}", sortingInfo.Property);
                return null;
            }
            return x;
        }

        private IQueryable<TEntity> GetOrderedQuery<T>(Expression<Func<TEntity, T>> expression, IQueryable<TEntity> orderedQuery, SortingInfo sortingInfo)
        {
            if (sortingInfo.Order)
            {
                return orderedQuery.OrderBy(expression);
            }

            return orderedQuery = orderedQuery.OrderByDescending(expression);
        }

        public PaginatedResponse<TEntity> FindByWithInclude(PaginationInfo paginationInfo, SortingInfo sortingInfo, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            PaginatedResponse<TEntity> response = new PaginatedResponse<TEntity>();
            IQueryable<TEntity> queryable = GetAllIncluding(includeProperties);
            PaginatedQueryable(paginationInfo, sortingInfo, queryable);

            response.PaginationInfo = paginationInfo;
            response.Items = queryable.ToList();

            return response;
        }

        private IQueryable<TEntity> GetAllIncluding
        (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> results = _dbSet.AsNoTracking()
              .Where(predicate).ToList();
            return results;
        }

        public Option<TEntity> FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
            //var entity = FindByKey(id);
            //_dbSet.Remove(entity);
            //_context.SaveChanges();
        }

    }
}
