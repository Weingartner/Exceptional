using System;
using System.Reactive.Subjects;

namespace Weingartner.Exceptional.Reactive.Subject
{
    public abstract class SubjectExceptionalBase<T> : ISubjectExceptional<T>
    {
        protected SubjectExceptionalBase(ISubject<IExceptional<T>> inner)
        {
            Subject = inner;
        }

        public ISubject<IExceptional<T>> Subject { get; }
        public IObservable<IExceptional<T>> Observable => Subject;
        public IObserver<IExceptional<T>> Observer => Subject;

        public IDisposable Subscribe(IObserverExceptional<T> observer)
        {
            return Subject.Subscribe(observer.Observer);
        }

        public void OnNext(IExceptional<T> t)
        {
            Subject.OnNext(t);
        }


        public void OnCompleted()
        {
            Subject.OnCompleted();
        }
    }
}