using System.Reactive.Subjects;

namespace Weingartner.Reactive.Subject
{
    public class BehaviorSubjectExceptional<T> : SubjectExceptionalBase<T>
    {
        public BehaviorSubjectExceptional(T value) 
            : base(new BehaviorSubject<IExceptional<T>>(Exceptional.Ok(value)))
        {
        }
    }
}