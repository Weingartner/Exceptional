using System.Reactive.Subjects;

namespace Weingartner.Exceptional.Reactive.Subject
{
    public class SubjectExceptional<T> : SubjectExceptionalBase<T>
    {
        public SubjectExceptional() : base(new Subject<IExceptional<T>>())
        {
        }
    }
}
