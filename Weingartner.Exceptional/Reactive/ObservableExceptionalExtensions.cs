using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Weingartner.Async;
using Unit = System.Reactive.Unit;

namespace Weingartner.Reactive
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

        public static IObservableExceptional<T> DistinctUntilChanged<T>(this IObservableExceptional<T> o) => 
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

        public static IObservableExceptional<T> Concat<T>(this IEnumerable<IObservableExceptional<T>> os)
        {
            return os.Select(o => o.Observable).Concat().ToObservableExceptional();
        }

        public static IObservableExceptional<T> StartWith<T>(this IObservableExceptional<T> o, T v)
        {
            return o.Observable.StartWith(Exceptional.Ok(v)).ToObservableExceptional();
        }

        public static IObservableExceptional<T> Throttle<T>(this IObservableExceptional<T> o, TimeSpan t) =>
            o.Observable.Throttle(t).ToObservableExceptional();

        public static IObservableExceptional<T> Switch<T>(this IObservableExceptional<Task<T>> o) =>
            o.Observable
            .Select(te => te.Select(t => t.ToTaskOfExceptional().ToObservable().ToObservableExceptional()))
            .ToObservableExceptional()
            .Switch();


        public static IObservableExceptional<Task<Unit>> Switch(this IObservableExceptional<Task> o) =>
            o.Select
                (async t =>
                {
                    await t;
                    return Unit.Default;
                });

        public static IObservableExceptional<T> TakeWhile<T>(this IObservableExceptional<T> o, Func<T, bool> predicate)
        {
            return o.Observable.TakeWhile(v => v.HasException || predicate(v.Value)).ToObservableExceptional();
        }

        public static IObservableExceptional<T> Finally<T>(this IObservableExceptional<T> o, Action fn) =>
            o.Observable.Finally(fn).ToObservableExceptional();

        public static Task<IExceptional<T>> ToTask<T>(this IObservableExceptional<T> o, CancellationToken t) =>
            o.Observable.ToTask(t);

        public static IObservable<bool> IsOk<T>(this IObservableExceptional<T> o) =>
            o.Observable.Select(v => !v.HasException);
        public static IObservable<T> WhereIsOk<T>(this IObservableExceptional<T> o) =>
            o.Observable.Where(v => !v.HasException).Select(v => v.Value);

        /// <Summary>
        /// If any of the inputs are in error then the output will contain an aggregate
        /// exception of the inputs. If the selector function throws then the output will
        /// contain that exception.
        /// </Summary>
        public static IObservableExceptional<IList<T>> CombineLatest<T>
            (this IEnumerable<IObservableExceptional<T>> t1)
        {
            return Observable
                .CombineLatest(t1.Select(t => t.Observable))
                .Select
                (es =>
                {
                    var r1 = es.FirstOrDefault(e => e.HasException);
                    return r1 == null
                        ? Exceptional.Ok(es.Select(e => e.Value).ToList())
                        : Exceptional.Fail<IList<T>>(r1.Exception);
                }).ToObservableExceptional();

        }

        public static IObservableExceptional<T> Merge<T>(this IEnumerable<IObservableExceptional<T>> list)
        {
            return list.Select(p => p.Observable).Merge().ToObservableExceptional();
        }

        public static IObservableExceptional<T> TakeUntil<T, TOther>(this IObservableExceptional<T> o,
            IObservableExceptional<TOther> other)
        {
            return o.Observable.TakeUntil(other.Observable).ToObservableExceptional();
        }

        public static IObservableExceptional<T> OfType<T>(this IObservableExceptional<object> o)
        {
            return o.Observable
                .Select(x => x.As<T>())
                .WhereIsSome()
                .ToObservableExceptional();
        }

        /// <summary>
        /// Flatten the IObservableExceptional of IEnumerable of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservableExceptional<T> SelectMany<T>(this IObservableExceptional<IEnumerable<T>> source)
        {
            return source.Observable.SelectMany
                         ( e => e.HasException
                               ? new[] {Exceptional.Fail<T>( e.Exception )}
                               : e.Value.Select( Exceptional.Ok ).ToArray() )
                         .ToObservableExceptional();
        }

        private static IObservable<T> WhereIsSome<T>(this IObservable<Option<T>> q)
        {
            return q.SelectMany(o => o.MatchObservable(Observable.Return, Observable.Empty<T>));
        }

    }
}