using System;
using System.Reactive.Linq;

namespace Weingartner.Exceptional.Reactive
{
    public static class ObservableExceptionalExtensions
    {

        public static IDisposable Subscribe<T>
            (this IObservableExceptional<T> o,
                Action<T> onNext,
                Action<Exception> onError = null,
                Action onCompleted = null) =>
                    o.Subscribe(ObserverExceptional.Create(onNext, onError, onCompleted));

        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<IExceptional<T>> o)
            => ObservableExceptional.Create(o);
        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<T> o)
            => ObservableExceptional.Create(o);

        public static void OnError<T>(this IObserverExceptional<T> o, Exception e) => o.OnNext(Exceptional.Fail<T>(e));
        public static void OnNext<T>(this IObserverExceptional<T> o, T e) => o.OnNext(Exceptional.Ok(e));


        public static IObservableExceptional<T> Do<T>(this IObservableExceptional<T> o, Action<IExceptional<T>> fn) =>
            o.Observable.Do(fn).ToObservableExceptional();

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

        public static IConnectableObservableExceptional<T> Replay<T>(this IObservableExceptional<T> o, int i) => 
            new ConnectableObservableExceptional<T>(o.Observable.Replay(i));

        public static IConnectableObservableExceptional<T> Publish<T>(this IObservableExceptional<T> o) => 
            new ConnectableObservableExceptional<T>(o.Observable.Publish());

        public static IObservableExceptional<T> DistictUntilChanged<T>(this IObservableExceptional<T> o) => 
            o.Observable.DistinctUntilChanged().ToObservableExceptional();

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
    }
}