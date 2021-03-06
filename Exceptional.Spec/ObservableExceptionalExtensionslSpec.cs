﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using FluentAssertions;
using ReactiveUI;
using Weingartner.Reactive;
using Weingartner.Reactive.Subject;
using Xunit;

namespace Weingartner.Spec
{


    public class Numbers : ReactiveObject
    {
        int _A;
        public int A 
        {
            get => _A;
            set => this.RaiseAndSetIfChanged(ref _A, value);
        }

        int _B;
        public int B 
        {
            get => _B;
            set => this.RaiseAndSetIfChanged(ref _B, value);
        }

        int _C;
        public int C 
        {
            get => _C;
            set => this.RaiseAndSetIfChanged(ref _C, value);
        }
        
    }

    public class ObservableExceptionalExtensionslSpec
    {

        [Fact]
        public void ErrorsCanBePropogated()
        {
            var numbers = new Numbers();

            var list = new List<double>();
            var errors = new List<Exception>();

            numbers
                .WhenAnyValue(p => p.A, p => p.B, Tuple.Create)
                .ToObservableExceptional()
                .Select(v => v.Item1/v.Item2)
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
        public void LinqCanBeFun()
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

            or
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
        public void LinqExceptionalCanBeFunReference()
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

            or
                .Subscribe(onNext: val => list.Add(val), onError: err => errors.Add(err));

            list.Count.Should().Be(1);

            numbers.A = 10;

            list.Count.Should().Be(2);

            numbers.B = 5;

            list.Count.Should().Be(4);

            numbers.C = 3;
            list.Count.Should().Be(8);

            errors.Count.Should().Be( 0 );

        }

        [Fact]
        public void LinqCanBeFunReference()
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

            or
                .Subscribe(onNext: val => list.Add(val), onError: err => errors.Add(err));

            list.Count.Should().Be(1);

            numbers.A = 10;

            list.Count.Should().Be(2);

            numbers.B = 5;

            list.Count.Should().Be(4);

            numbers.C = 3;
            list.Count.Should().Be(8);

            errors.Count.Should().Be( 0 );

        }
        #endregion

        [Fact]
        public void DistinctUntilChangedWillWork()
        {
            var s = new BehaviorSubjectExceptional<int>(0);


            var list = new List<int>();
            var elist = new List<Exception>();

            s.DistinctUntilChanged()
                .Subscribe(v => list.Add(v), onError:e=>elist.Add(e));

            s.OnNext(0); 
            s.OnNext(1); 
            s.OnNext(2);

            list.Should().BeEquivalentTo(0, 1, 2);

            s.OnNext(2);
            s.OnNext(2);
            s.OnNext(3);

            list.Should().BeEquivalentTo(0, 1, 2, 3);

            // Note that the error introduces a new event between the two 3's
            // so that they are no longer distinct.
            s.OnError(new Exception("Foo"));

            s.OnNext(3);


            list.Should().BeEquivalentTo(0, 1, 2, 3, 3);

            elist.Count.Should().Be(1);


        }


        [Fact]
        public void CombineLatestShouldWork()
        {
            var s0 = new BehaviorSubjectExceptional<int>(0);
            var s1 = new BehaviorSubjectExceptional<int>(1);

            var s3 = ObservableExceptional.CombineLatest(s0, s1, Tuple.Create);


            var list = new List<Tuple<int,int>>();
            var elist = new List<Exception>();

            s3.Subscribe(v => list.Add(v), err => elist.Add(err));

            list.Should().BeEquivalentTo(Tuple.Create(0, 1));

            s1.OnNext(5);

            list.Should().BeEquivalentTo(Tuple.Create(0, 1), Tuple.Create(0,5));

            s1.OnError(new Exception("111"));
            list.Should().BeEquivalentTo(Tuple.Create(0, 1), Tuple.Create(0,5));
            elist.Select(m=>m.Message).Should().BeEquivalentTo("111");

            s0.OnError(new Exception("000"));

            list.Should().BeEquivalentTo(Tuple.Create(0, 1), Tuple.Create(0,5));
            elist.Select(m=>m.Message).Should().BeEquivalentTo("111", "One or more errors occurred.");

            s0.OnNext(10);

            list.Should().BeEquivalentTo(Tuple.Create(0, 1), Tuple.Create(0,5));
            elist.Select(m=>m.Message).Should().BeEquivalentTo("111", "One or more errors occurred.", "111");

            s1.OnNext(20);

            list.Should().BeEquivalentTo(Tuple.Create(0, 1), Tuple.Create(0,5), Tuple.Create(10,20));
            elist.Select(m=>m.Message).Should().BeEquivalentTo("111", "One or more errors occurred.", "111");
        }

        [Fact]
        public void OfTypeShouldWork()
        {
            var obs = Observable.Create<IExceptional<A>>(observer =>
            {
                observer.OnNext(Exceptional.Ok<A>(new C()));
                observer.OnNext(Exceptional.Fail<C>("error"));
                observer.OnNext(Exceptional.Ok<A>(new B()));
                observer.OnNext(Exceptional.Fail<B>("error"));
                observer.OnCompleted();

                return Disposable.Empty;
            })
            .ToObservableExceptional();

            var items = obs.OfType<C>().Observable.ToEnumerable().ToList();
            items.Count.Should().Be(3);
            items[0].HasException.Should().BeFalse();
            items[1].HasException.Should().BeTrue();
            items[2].HasException.Should().BeTrue();
        }

        private abstract class A
        {
        }
        private class B : A
        {
        }
        private class C : A
        {
        }
    }
}