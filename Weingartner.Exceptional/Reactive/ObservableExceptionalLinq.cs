using System;
using System.Reactive.Linq;

namespace Weingartner.Reactive
{
    public static class ObservableExceptionalLinq
    {
        public static IObservableExceptional<TU> Select<TU, TR>(this IObservableExceptional<TR> o, Func<TR, TU> fn)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (fn == null)
                throw new ArgumentNullException(nameof(fn));

            return o.Observable.Select(v => v.Select(fn)).ToObservableExceptional();
        }

        public static IObservableExceptional<TU> SelectMany<TU, TR>
            (this IObservableExceptional<TR> o, Func<TR, IObservableExceptional<TU>> fn)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (fn == null)
                throw new ArgumentNullException(nameof(fn));

            return o.Observable
                .SelectMany(te => te.Select(fn).Flatten().Observable)
                .ToObservableExceptional();

        }

        public static IObservableExceptional<TV> SelectMany<T, TU, TV>(this IObservableExceptional<T> o, Func<T, IObservableExceptional<TU>> fn0, Func<T, TU, TV> fn1)
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

        internal static IObservableExceptional<T> Flatten<T>(this IExceptional<IObservableExceptional<T>> v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            return v.HasException ? ObservableExceptional.Fail<T>(v.Exception) : v.Value;
        }
    }
}