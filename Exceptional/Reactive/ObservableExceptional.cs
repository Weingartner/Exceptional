using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weingartner.Exceptional.Async;

namespace Weingartner.Exceptional.Reactive
{
    public static class ObservableExceptional
    {
        public static IObservableExceptional<T> Create<T>(IObservable<T> o )=> new ObservableExceptional<T>(o);
        public static IObservableExceptional<T> Create<T>(IObservable<IExceptional<T>> o )=> new ObservableExceptional<T>(o);
        public static IObservableExceptional<T> Return<T>(T o) => Create(Observable.Return(o));
        public static IObservableExceptional<T> Fail<T>(Exception e) => Create(Observable.Return(Exceptional.Fail<T>(e)));
        public static IObservableExceptional<T> Empty<T>() => Create(Observable.Empty<T>());

        public static IObservableExceptional<T> FromAsync<T>(Func<CancellationToken, Task<T>> functionAsync) => Observable.FromAsync
            (token => functionAsync(token).ToTaskOfExceptional()).ToObservableExceptional();

        public static IObservableExceptional<TSource> ObserveOn<TSource>
            (this IObservableExceptional<TSource> source, IScheduler scheduler) => source.Observable.ObserveOn(scheduler).ToObservableExceptional();

        public static IObservableExceptional<IList<TSource>> Buffer<TSource>
            (this IObservableExceptional<TSource> source, int count, int skip)
        {
            return source
                .Observable
                .Buffer(count, skip)
                .Select(buffer =>
                {
                    var ex1 = buffer.FirstOrDefault(b => b.HasException);
                    return ex1 != null 
                        ? Exceptional.Fail<IList<TSource>>(ex1.Exception) 
                        : Exceptional.Ok(buffer.Select(b=>b.Value).ToList());
                })
                .ToObservableExceptional();
        }


        public static IObservableExceptional<T> Create<T>(Func<IObserverExceptional<T>, IDisposable> fn)
        {
            return Create
                (Observable.Create<IExceptional<T>>
                    (observer =>
                    {
                        var observerex = new ObserverExceptional<T>(observer);
                        return fn(observerex);
                    }));
        }
    }

    internal class ObservableExceptional<T> : IObservableExceptional<T>
    {
        public void Subscribe(IObserverExceptional<T> observer)
        {
            Observable.Subscribe(observer.Observer);
        }

        public IObservable<IExceptional<T>> Observable { get; }

        public ObservableExceptional(IObservable<T> inner)
        {
            Observable = inner.Select<T, IExceptional<T>>(Exceptional.Ok);
        }
        public ObservableExceptional(IObservable<IExceptional<T>> inner)
        {
            Observable = inner;
        }


    }
}