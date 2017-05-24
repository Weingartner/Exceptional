using System;

namespace Weingartner.Reactive
{
    public interface IObserverExceptional<in T>
    {
        void OnNext(IExceptional<T> t);
        void OnCompleted();
        IObserver<IExceptional<T>> Observer { get; }
    }
}