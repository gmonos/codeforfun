using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Utilities.Functional
{
    public static class ErrorExt
    {
        public static Either<Error, R> ToEither<R>(this R value) => value;
        public static Either<Error, R> ToEither<R>(this Error err) => err;
    }
}
