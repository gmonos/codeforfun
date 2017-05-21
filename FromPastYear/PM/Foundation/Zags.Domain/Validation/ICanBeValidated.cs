using System.Collections.Generic;
using Zags.Utilities;
using Zags.Utilities.Functional;

namespace Zags.Domain.Validation
{
    public interface ICanBeValidated<T>
    {
        Either<List<Error>, T> Validate();
    }
}
