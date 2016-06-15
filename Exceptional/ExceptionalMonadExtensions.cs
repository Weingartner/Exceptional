using System;

namespace Weingartner.Exceptional
{
    public static class ExceptionalMonadExtensions
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

        public static IExceptional<U> Select<T, U>(this IExceptional<T> value, Func<T, U> k)
        {
            return (value.HasException)
                ? new Exceptional<U>(value.Exception)
                : Exceptional.Execute(k, value.Value);
        }

        public static IExceptional<U> SelectMany<T, U>(this IExceptional<T> value, Func<T, IExceptional<U>> k)
        {
            if (value.HasException) return new Exceptional<U>(value.Exception);
            else
            {
                var exceptional = Exceptional.Execute(k, value.Value);

                if (exceptional.HasException)
                    return new Exceptional<U>(exceptional.Exception);

                return exceptional.Value;
            }
        }

        public static IExceptional<V> SelectMany<T, U, V>(this IExceptional<T> value, Func<T, IExceptional<U>> k, Func<T, U, V> m)
        {
            return value.SelectMany(t => k(t).SelectMany(u => m(t, u).ToExceptional()));
        }
    }
}