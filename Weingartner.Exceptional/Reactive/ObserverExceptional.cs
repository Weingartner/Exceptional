using System;

namespace Weingartner.Exceptional.Reactive
{
    public static class ObserverExceptional
    {
        public static IObserverExceptional<T> Create<T>
            (Action<T> onNext, Action<Exception> onError = null, Action onCompletion = null) =>
                new ObserverExceptional<T>(onNext, onError, onCompletion);
    }

    internal class ObserverExceptional<T> : IObserverExceptional<T>
    {
        public IObserver<IExceptional<T>> Observer { get; }

        public ObserverExceptional(IObserver<IExceptional<T>> inner)
        {
            Observer = inner;
        }

        public ObserverExceptional(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {

            Observer = System.Reactive.Observer.Create<IExceptional<T>>
                (onNext: v =>
                {
                    if (v.HasException)
                        onError?.Invoke(v.Exception);
                    else
                        onNext?.Invoke(v.Value);
                }
                    , onError: _ => {}
                    , onCompleted: onCompleted ?? (()=> {})
                );
        }

        public void OnNext(IExceptional<T> t)
        {
            Observer.OnNext(t);
        }

        public void OnCompleted()
        {
            Observer.OnCompleted();
        }
    }
}