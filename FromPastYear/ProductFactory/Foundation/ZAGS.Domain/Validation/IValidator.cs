using System.Collections.Generic;
using Zags.Utility.Errors;
using Zags.Utility.Functional;

namespace ZAGS.Domain.Validation
{
    public interface IValidator<T>
    {
        Either<IList<Error>, T> Validate(T entity);
    }
}
