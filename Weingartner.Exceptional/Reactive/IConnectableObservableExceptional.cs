using System;
using System.Reactive.Subjects;

namespace Weingartner.Exceptional.Reactive
{
    public interface IConnectableObservableExceptional<out T> : IObservableExceptional<T>
    {
        /// <summary>
        /// Connects the observable wrapper to its source. All subscribed observers will receive values from the underlying observable sequence as long as the connection is established.
        /// </summary>
        /// <returns>Disposable used to disconnect the observable wrapper from its source, causing subscribed observer to stop receiving values from the underlying observable sequence.</returns>
        IDisposable Connect();

        IConnectableObservable<IExceptional<T>> ConnectableObservable { get; }
    }
}