using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Weingartner.Exceptional.ObservableExceptional;

namespace Weingartner.Exceptional
{
    public interface IObservableExceptional<T>
    {
        void Subscribe(IObserverExceptional<T> observer);

        IObservable<IExceptional<T>> Observable { get; }
    }

    public interface IObserverExceptional<T>
    {
        void OnNext(IExceptional<T> t);
        void OnCompleted();
        IObserver<IExceptional<T>> Observer { get; }
    }

    internal class ObservableExceptional<T> : IObservableExceptional<T>
    {
        public void Subscribe(IObserverExceptional<T> observer)
        {
            Observable.Subscribe(observer.Observer);
        }

        public IObservable<IExceptional<T>> Observable { get; }

        public ObservableExceptional(IObservable<T> inner)
        {
            Observable = inner.Select(Exceptional.Ok);
        }
        public ObservableExceptional(IObservable<IExceptional<T>> inner)
        {
            Observable = inner;
        }


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

    public static class ObserverExceptional
    {
        public static IObserverExceptional<T> Create<T>
            (Action<T> onNext, Action<Exception> onError = null, Action onCompletion = null) =>
                new ObserverExceptional<T>(onNext, onError, onCompletion);
    }

    public static class ObservableExceptional
    {

        public static void Subscribe<T>
            (this IObservableExceptional<T> o,
                Action<T> onNext,
                Action<Exception> onError = null,
                Action onCompleted = null) =>
                    o.Subscribe(ObserverExceptional.Create(onNext, onError, onCompleted));

        public static IObservableExceptional<T> Create<T>(IObservable<T> o )=> new ObservableExceptional<T>(o);
        public static IObservableExceptional<T> Create<T>(IObservable<IExceptional<T>> o )=> new ObservableExceptional<T>(o);
        public static IObservableExceptional<T> Return<T>(T o) => Create(Observable.Return(o));
        public static IObservableExceptional<T> Fail<T>(Exception e) => Create(Observable.Return(Exceptional.Fail<T>(e)));

        public static IObservableExceptional<T> Create<T>(Func<IObserverExceptional<T>, IDisposable> fn)
        {
            return Create
                (Observable.Create<IExceptional<T>>
                    (observer =>
                    {
                        var observerex = new ObserverExceptional<T>(observer);
                        return fn(observerex);
                    }));
        }

        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<IExceptional<T>> o)
            => Create(o);
        public static IObservableExceptional<T> ToObservableExceptional<T>(this IObservable<T> o)
            => Create(o);

        public static IObservableExceptional<TU> Select<TU, TR>(this IObservableExceptional<TR> o, Func<TR, TU> fn) =>
            o.Observable.Select(v => v.Select(fn)).ToObservableExceptional();

        public static IObservableExceptional<TU> SelectMany<TU, TR>
            (this IObservableExceptional<TR> o, Func<TR, IObservableExceptional<TU>> fn)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (fn == null)
                throw new ArgumentNullException(nameof(fn));

            var oU =
                from ve in o.Observable
                from v in ve
                from qe in fn(v).Observable
                select qe;

            return oU.ToObservableExceptional();
        }


        public static IObservableExceptional<V> SelectMany<T, U, V>(this IObservableExceptional<T> o, Func<T, IObservableExceptional<U>> fn0, Func<T, U, V> fn1)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (fn0 == null)
                throw new ArgumentNullException(nameof(fn0));
            if (fn1 == null)
                throw new ArgumentNullException(nameof(fn1));

            var r = 
                from ve in o.Observable
                from v in ve
                from qe in fn0(v).Observable
                select (qe.Select(u=>fn1(v,u)));

            return r.ToObservableExceptional();

        }

        public static IObservableExceptional<T> Switch<T>(this IObservableExceptional<IObservableExceptional<T>> o )
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));

            var o2 =
                o
                    .Observable
                    .Select(v => (v.HasException ? Fail<T>(v.Exception) : v.Value).Observable)
                    .Switch()
                    .ToObservableExceptional();

            return o2;
        }
    }
}
