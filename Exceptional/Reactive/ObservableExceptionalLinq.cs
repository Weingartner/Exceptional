using System;
using System.Reactive.Linq;

namespace Weingartner.Exceptional.Reactive
{
    public static class ObservableExceptionalLinq
    {
        public static IObservableExceptional<TU> Select<TU, TR>(this IObservableExceptional<TR> o, Func<TR, TU> fn) =>
            o.Observable.Select(v => ExceptionalLinq.Select<TR, TU>(v, fn)).ToObservableExceptional();

        public static IObservableExceptional<TU> SelectMany<TU, TR>
            (this IObservableExceptional<TR> o, Func<TR, IObservableExceptional<TU>> fn)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (fn == null)
                throw new ArgumentNullException(nameof(fn));

            return o.Observable.SelectMany
                (te => te.Select<TR, IObservableExceptional<TU>>(fn).IfErrorUnsafe(ObservableExceptional.Fail<TU>).Observable).ToObservableExceptional();

        }

        public static IObservableExceptional<V> SelectMany<T, U, V>(this IObservableExceptional<T> o, Func<T, IObservableExceptional<U>> fn0, Func<T, U, V> fn1)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (fn0 == null)
                throw new ArgumentNullException(nameof(fn0));
            if (fn1 == null)
                throw new ArgumentNullException(nameof(fn1));


            return o
                .Observable
                .SelectMany
                ( te => te.Select(fn0).Flatten().Observable
                    , (te,ue)=> te.SelectMany(t => ue, fn1)
                )
                .ToObservableExceptional();

        }

        /// <summary>
        /// Filters the values. Will always pass through errors even though no test can be applied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IObservableExceptional<T> Where<T>(this IObservableExceptional<T> o, Func<T, bool> fn)
        {
            return o.Observable.Where(e => e.HasException || fn(e.Value)).ToObservableExceptional();
        }

        public static IObservableExceptional<T> Switch<T>(this IObservableExceptional<IObservableExceptional<T>> o )
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));

            return o
                .Observable
                .Select(v => v.Flatten().Observable)
                .Switch()
                .ToObservableExceptional();
        }

        private static IObservableExceptional<T> Flatten<T>(this IExceptional<IObservableExceptional<T>> v)
        {
            return v.HasException ? ObservableExceptional.Fail<T>(v.Exception) : v.Value;
        }
    }
}