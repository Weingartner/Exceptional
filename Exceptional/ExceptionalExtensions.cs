using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Weingartner.Exceptional
{
    public static class ExceptionalExtensions
    {
        public static IExceptional<U> ThenExecute<T, U>(this IExceptional<T> value, Func<T, U> getValue)
        {
            return value.SelectMany(x => Exceptional.Execute(() => getValue(x)));
        }

        public static IObservable<U> WithoutErrors<U>(this IObservable<IExceptional<U>> o)
        {
            return o.Where(v => !v.HasException).Select(v => v.Value);
        }

        public static T Catch<T>(this IExceptional<T> e, Func<Exception, T> fn)
        {
            if (e.HasException)
            {
                return fn(e.Exception);
            }
            return e.Value;
        }
        public static void Catch<T>(this IExceptional<T> e, Action<Exception> fn)
        {
            if (e.HasException)
            {
                fn(e.Exception);
            }
        }
        public static T IfErrorUnsafe<T>(this IExceptional<T> e,Func<Exception, T> fn) => e.HasException ? fn(e.Exception) : e.Value;

        /// <summary>
        /// If any of the internal exceptional have an error then the output exceptional is in error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="this"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IExceptional<IReadOnlyList<T>> AggregateExceptionals<T, U>
            (this IEnumerable<IExceptional<U>> @this, Func<U, T> fn)
        {
            var list = @this.ToList();
            var exceptions = list.Where(v => v.HasException).Select(v => v.Exception).ToList();
            if (exceptions.Count == 1)
                return new Exceptional<IReadOnlyList<T>>(exceptions[0]);
            if (exceptions.Count > 1)
                return new Exceptional<IReadOnlyList<T>>(new AggregateException(exceptions));

            return new Exceptional<IReadOnlyList<T>>(list.Select(v => fn(v.Value)).ToList());

        }
    }
}