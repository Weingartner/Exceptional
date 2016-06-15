using System;

namespace Weingartner.Exceptional.Reactive
{
    public interface IObserverExceptional<T>
    {
        void OnNext(IExceptional<T> t);
        void OnCompleted();
        IObserver<IExceptional<T>> Observer { get; }
    }
}