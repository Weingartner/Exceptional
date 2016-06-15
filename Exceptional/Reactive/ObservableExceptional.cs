using System;
using System.Reactive.Linq;

namespace Weingartner.Exceptional.Reactive
{
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