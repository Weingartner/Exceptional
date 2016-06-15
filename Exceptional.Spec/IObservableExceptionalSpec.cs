using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using FluentAssertions;
using ReactiveUI;
using Xunit;

namespace Weingartner.Exceptional.Spec
{


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

    public class IObservableExceptionalSpec
    {

        [Fact]
        public void ErrorsCanBePropogated()
        {
            var numbers = new Numbers();

            var list = new List<double>();
            var errors = new List<Exception>();

            int count=0;
            numbers
                .WhenAnyValue(p => p.A, p => p.B, Tuple.Create)
                .ToObservableExceptional()
                .Select(v => v.Item1/v.Item2)
                .Do(_=>count++)
                .Subscribe(onNext: val=>list.Add(val), onError:err=>errors.Add(err));


            list.Count.Should().Be(0);
            errors.Count.Should().Be(1);

            numbers.A = 10;

            list.Count.Should().Be(0);
            errors.Count.Should().Be(2);

            numbers.B = 5;

            list.Count.Should().Be(1);
            list[0].Should().Be(2.0);
            errors.Count.Should().Be(2);
        }

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

        #region these two tests should model similar behaviour
        [Fact]
        public void LINQExceptionalCanBeFunReference()
        {
            var numbers = new Numbers();

            var list = new List<object>();
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
                select new {a,b,c};

            int count=0;
            or
                .Do(_=>count++)
                .Subscribe(onNext: val => list.Add(val), onError: err => errors.Add(err));

            list.Count.Should().Be(1);

            numbers.A = 10;

            list.Count.Should().Be(2);

            numbers.B = 5;

            list.Count.Should().Be(4);

            numbers.C = 3;
            list.Count.Should().Be(8);

        }

        [Fact]
        public void LINQCanBeFunReference()
        {
            var numbers = new Numbers();

            var list = new List<object>();
            var errors = new List<Exception>();

            var oa =
                numbers
                    .WhenAnyValue(p => p.A);
            var ob =
                numbers
                    .WhenAnyValue(p => p.B);

            var oc =
                numbers
                    .WhenAnyValue(p => p.C);

            var or = 
                from a in oa
                from b in ob
                from c in oc
                select new {a,b,c};

            int count=0;
            or
                .Do(_=>count++)
                .Subscribe(onNext: val => list.Add(val), onError: err => errors.Add(err));

            list.Count.Should().Be(1);

            numbers.A = 10;

            list.Count.Should().Be(2);

            numbers.B = 5;

            list.Count.Should().Be(4);

            numbers.C = 3;
            list.Count.Should().Be(8);

        }
        #endregion


    }
}