using System;
using System.Collections.Generic;
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
        
    }

    public class IObservableExceptionalSpec
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

        
    }
}