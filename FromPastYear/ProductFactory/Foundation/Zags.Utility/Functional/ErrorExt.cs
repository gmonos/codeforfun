using Zags.Utility.Errors;

namespace Zags.Utility.Functional
{
    public static class ErrorExt
    {
        public static Either<Error, R> ToEither<R>(this R value) => value;
        public static Either<Error, R> ToEither<R>(this Error err) => err;
    }
}
