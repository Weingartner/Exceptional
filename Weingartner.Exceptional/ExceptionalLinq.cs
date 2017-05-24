using System;

namespace Weingartner
{
    public static class ExceptionalLinq
    {
        public static IExceptional<T> ToExceptional<T>(this T value)
        {
            return new Exceptional<T>(value);
        }

        public static IExceptional<T> ToExceptional<T>(this Exception ex)
        {
            return new Exceptional<T>(ex);
        }

        public static IExceptional<T> ToExceptional<T>(this Func<T> getValue)
        {
            return new Exceptional<T>(getValue);
        }

        public static IExceptional<TU> Select<T, TU>(this IExceptional<T> value, Func<T, TU> k)
        {
            return (value.HasException)
                ? new Exceptional<TU>(value.Exception)
                : Exceptional.Execute(k, value.Value);
        }

        public static IExceptional<TU> SelectMany<T, TU>(this IExceptional<T> value, Func<T, IExceptional<TU>> k)
        {
            if (value.HasException) return new Exceptional<TU>(value.Exception);
            else
            {
                var exceptional = Exceptional.Execute(k, value.Value);

                if (exceptional.HasException)
                    return new Exceptional<TU>(exceptional.Exception);

                return exceptional.Value;
            }
        }

        public static IExceptional<TV> SelectMany<T, TU, TV>(this IExceptional<T> value, Func<T, IExceptional<TU>> k, Func<T, TU, TV> m)
        {
            return value.SelectMany(t => k(t).SelectMany(u => m(t, u).ToExceptional()));
        }
    }
}