using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zags.Utilities.Functional;

namespace Zags.DataAccess.EF
{
    public static class Utilities
    {
        public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(int id)
        {
            var item = Expression.Parameter(typeof(TEntity), "entity");
            var prop = Expression.Property(item, typeof(TEntity).Name + "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
            return lambda;
        }

        public static Option<Expression<Func<TEntity, TResult>>> BuildLambdaForSorting<TEntity, TResult>(Type type, string propName)
        {
            try
            {
                ParameterExpression param = Expression.Parameter(type, "_");
                var prop = Expression.Property(param, propName);
                var lambda = Expression.Lambda<Func<TEntity, TResult>>(prop, param);
                return lambda;
            }
            catch (ArgumentException)
            {
                return null;
            }
            
        }
    }
}
