using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Weingartner
{
    public static class ExceptionalExtensions
    {
        public static IExceptional<TU> ThenExecute<T, TU>(this IExceptional<T> value, Func<T, TU> getValue)
        {
            return value.SelectMany(x => Exceptional.Execute(() => getValue(x)));
        }

        public static IObservable<TU> WithoutErrors<TU>(this IObservable<IExceptional<TU>> o)
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
        /// <typeparam name="TU"></typeparam>
        /// <param name="this"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IExceptional<IReadOnlyList<T>> AggregateExceptionals<T, TU>
            (this IEnumerable<IExceptional<TU>> @this, Func<TU, T> fn)
        {
            var list = @this.ToList();
            var exceptions = list.Where(v => v.HasException).Select(v => v.Exception).ToList();
            if (exceptions.Count == 1)
                return new Exceptional<IReadOnlyList<T>>(exceptions[0]);
            if (exceptions.Count > 1)
                return new Exceptional<IReadOnlyList<T>>(new AggregateException(exceptions));

            return new Exceptional<IReadOnlyList<T>>(list.Select(v => fn(v.Value)).ToList());

        }

        public static Option<IExceptional<T>> As<T>(this IExceptional<object> x)
        {
            return x.Match(
                o => x.Value is T ? Some(Exceptional.Ok((T)x.Value)) : None,
                e => Some(Exceptional.Fail<T>(x.Exception))
                );
        }

        public static TOut Match<TIn, TOut>(
            this IExceptional<TIn> x,
            Func<TIn, TOut> okFn,
            Func<Exception, TOut> failFn)
        {
            if (x.HasException)
                return failFn(x.Exception);
            return okFn(x.Value);
        }
    }
}