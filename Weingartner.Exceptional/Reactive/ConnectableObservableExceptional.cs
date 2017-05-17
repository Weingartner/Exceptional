using System;
using System.Reactive.Subjects;

namespace Weingartner.Exceptional.Reactive
{
    internal class ConnectableObservableExceptional<T> : ObservableExceptional<T>, IConnectableObservableExceptional<T>
    {

        public ConnectableObservableExceptional(IConnectableObservable<IExceptional<T>> connectableObservable) : base (connectableObservable)
        {
            ConnectableObservable = connectableObservable;
        }

        public IDisposable Connect()
        {
            return ConnectableObservable.Connect();
        }

        public IConnectableObservable<IExceptional<T>> ConnectableObservable { get; }

    }
}