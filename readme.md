Exceptional [on nuget](https://www.nuget.org/packages/Weingartner.Exceptional)
===========

Exceptional is a LINQ compatible error handling monad. Sometimes errors should be carried around like data rather than letting them bubble up as exception. 

The following tests cases show some use cases.

    [Fact]
    public void SelectManyIsExceptionSafe()
    {
        var xa = Exceptional.Success(1);
        var xb = Exceptional.Success(2);

        Func<double, double, double> fn = (a, b) =>
        {
            throw new Exception("foo");
        };

        var xx =
            from a in xa
            from b in xb
            select fn(a, b);


        xx.HasException.Should().BeTrue();
        xx.Exception.Message.Should().Be("foo");
        new Action
            (() =>
            {
                var foo = xx.Value;
            }).ShouldThrow<Exception>();
        
    }

    [Fact]
    public void SelectIsExceptionSafe()
    {
        var xa = Exceptional.Success(1);
        var xb = Exceptional.Success(2);

        Func<double, double, double> fn = (a, b) =>
        {
            throw new Exception("foo");
        };

        var xx =
            from a in xa
            select fn(a, 2);


        xx.HasException.Should().BeTrue();
        xx.Exception.Message.Should().Be("foo");
        new Action
            (() =>
            {
                var foo = xx.Value;
            }).ShouldThrow<Exception>();
        
    }

IObservableExceptional [on nuget](https://www.nuget.org/packages/Weingartner.Exceptional)
======================

Reactive extensions has a big problem when using it for user interface programming. Exceptions terminate subscriptions. The
standard Catch and Retry techniques don't work well with ReactiveUI. Retrying on INPC backed observables will just get
the error to trigger again which is generally not what you want.

What we really want is to carry the error along as if it is data and deal with it at the consumer end. We don't
want the event sources to stop listening for mouse clicks or data changes.

Here I propose a wrapper around RX using the above Exceptional class. Ther new interfaces are

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

and have a growing set of LINQ compatible operators so you don't have to worry about handling errors
until you actually want to handle the errors. For example from my test classes

    public class Numbers : ReactiveObject
    {
        int _A;
        public int A 
        {
            get { return _A; }
            set { this.RaiseAndSetIfChanged(ref _A, value); }
        }

        int _B;
        public int B 
        {
            get { return _B; }
            set { this.RaiseAndSetIfChanged(ref _B, value); }
        }

        int _C;
        public int C 
        {
            get { return _C; }
            set { this.RaiseAndSetIfChanged(ref _C, value); }
        }
        
    }

I have a test case

        [Fact]
        public void LINQCanBeFun()
        {
            var numbers = new Numbers();

            var list = new List<double>();
            var errors = new List<Exception>();

            var oa =
                numbers
                    .WhenAnyValue(p => p.A)
                    .ToObservableExceptional();
            var ob =
                numbers
                    .WhenAnyValue(p => p.B)
                    .ToObservableExceptional();

            var oc =
                numbers
                    .WhenAnyValue(p => p.C)
                    .ToObservableExceptional();

            var or = 
                from a in oa
                from b in ob
                from c in oc
                select (a+b)/c;

            int count=0;
            or
                .Do(_=>count++)
                .Subscribe(onNext: val => list.Add(val), onError: err => errors.Add(err));

            list.Count.Should().Be(0);
            errors.Count.Should().Be(1);

            numbers.A = 10;

            list.Count.Should().Be(0);
            errors.Count.Should().Be(2);

            numbers.B = 5;

            list.Count.Should().Be(0);
            errors.Count.Should().Be(4);

            numbers.C = 3;
            list.Count.Should().Be(4);
            list[3].Should().Be(5);
            errors.Count.Should().Be(4);

        }

Note that errors can be collected in the subscribe but they do not terminate the sequence.

If your favorite IObservable combinators are missing either create a pull request or open an issue. 



