using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zags.DataAccess;
using Zags.Domain;

namespace Zags.DataAccess.EF
{
    public interface IEFGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        PaginatedResponse<TEntity> FindByWithInclude(PaginationInfo paginationInfo, SortingInfo sortingInfo, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
