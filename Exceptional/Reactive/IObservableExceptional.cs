using System;

namespace Weingartner.Exceptional.Reactive
{
    public interface IObservableExceptional<T>
    {
        IDisposable Subscribe(IObserverExceptional<T> observer);

        IObservable<IExceptional<T>> Observable { get; }
    }
}
