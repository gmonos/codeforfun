using System;
using System.Linq.Expressions;
using Zags.Domain;
using Zags.Utilities.Functional;

namespace Zags.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        PaginatedResponse<TEntity> FindBy(PaginationInfo paginationInfo, SortingInfo sortingInfo, Expression<Func<TEntity, bool>> predicate);
        Option<TEntity> FindById(int id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        int Delete(int id);
    }
}
