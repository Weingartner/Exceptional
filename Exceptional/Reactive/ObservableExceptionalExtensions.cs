using System;
using System.Reactive.Linq;

namespace Weingartner.Exceptional.Reactive
{
    public static class ObservableExceptionalExtensions
    {

        public static void Subscribe<T>
            (this IObservableExceptional<T> o,
                Action<T> onNext,
                Action<Exception> onError = null,
                Action onCompleted = null) =>
                    o.Subscribe(ObserverExceptional.Create(onNext, onError, onCompleted));

        public static IObservableExceptional<T> Create<T>(IObservable<T> o )=> new ObservableExceptional<T>(o);
        public static IObservableExceptional<T> Create<T>(IObservable<IExceptional<T>> o )=> new ObservableExceptional<T>(o);
        public static IObservableExceptional<T> Return<T>(T o) => Create(Observable.Return(o));
        public static IObservableExceptional<T> Fail<T>(Exception e) => Create(Observable.Return(Exceptional.Fail<T>(e)));

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

        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<IExceptional<T>> o)
            => Create(o);
        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<T> o)
            => Create(o);



        public static IObservableExceptional<T> Do<T>(this IObservableExceptional<T> o, Action<IExceptional<T>> fn) =>
            o.Observable.Do(fn).ToObservableExceptional();
    }
}