using System;
using Zags.Utility.Errors;

namespace Zags.Utility.Functional
{
    public static partial class F
    {
        private static readonly Unit unit = new Unit();
        // Unit
        public static Unit Unit() => unit;

        //Error
        public static Error Error(string message) => new Error(message);

        // Option
        public static Option<T> Some<T>(T value) => Option.Of(value);
        public static readonly NoneType None = NoneType.Default;

        public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> func)
         => t1 => t2 => func(t1, t2);

        public static Func<T1, Func<T2, Func<T3, R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> func)
            => t1 => t2 => t3 => func(t1, t2, t3);

        public static Func<T1, Func<T2, T3, R>> CurryFirst<T1, T2, T3, R>
           (this Func<T1, T2, T3, R> @this) => t1 => (t2, t3) => @this(t1, t2, t3);

    }
}
