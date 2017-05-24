using System.Reactive.Linq;

namespace Weingartner.Reactive
{
    public static class ConnectableObservableExceptionalExtensions
    {
        public static IObservableExceptional<T> RefCount<T>(this IConnectableObservableExceptional<T> o) =>
            o.ConnectableObservable.RefCount().ToObservableExceptional();
    }
}
