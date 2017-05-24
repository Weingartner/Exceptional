using System.Reactive.Subjects;

namespace Weingartner.Reactive.Subject
{
    public interface ISubjectExceptional<T> : IObservableExceptional<T>, IObserverExceptional<T>
    {
        ISubject<IExceptional<T>> Subject { get; }
    }
}