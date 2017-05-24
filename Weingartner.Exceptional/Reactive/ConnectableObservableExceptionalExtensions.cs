using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weingartner.Reactive
{
    public static class ConnectableObservableExceptionalExtensions
    {
        public static IObservableExceptional<T> RefCount<T>(this IConnectableObservableExceptional<T> o) =>
            o.ConnectableObservable.RefCount().ToObservableExceptional();
    }
}
