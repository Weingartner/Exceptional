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

        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<IExceptional<T>> o)
            => ObservableExceptional.Create(o);
        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<T> o)
            => ObservableExceptional.Create(o);



        public static IObservableExceptional<T> Do<T>(this IObservableExceptional<T> o, Action<IExceptional<T>> fn) =>
            o.Observable.Do(fn).ToObservableExceptional();
    }
}