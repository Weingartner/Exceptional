using System;
using FluentAssertions;
using Xunit;

namespace Weingartner.Exceptional.Spec
{
    public class ExceptionalSpec
    {
        [Fact]
        public void WhenNoInputsHaveExceptionsShouldEvaluateSelector()
        {
            var a = 1.ToExceptional();
            var b = 2.ToExceptional();

            var r = Exceptional.Combine(a, b, (x, y) => x + y);

            r.HasException.Should().BeFalse();
            r.Value.Should().Be(3);
        }

        [Fact]
        public void WhenMultipleInputsHaveExceptionTheyShouldBeAggregated()
        {
            var a = 1.ToExceptional();
            var b = Exceptional.Failure<int>(new Exception("AAA"));
            var c = Exceptional.Failure<int>(new Exception("BBB"));

            var r = Exceptional.Combine(a, b, c, (x, y, z) => x + y + z);

            r.HasException.Should().BeTrue();
            var va = r.Exception as AggregateException;

            va.Should().NotBeNull();
            va.InnerExceptions.Count.Should().Be(2);
            va.InnerExceptions.Should().Contain(b.Exception);
            va.InnerExceptions.Should().Contain(c.Exception);
        }

        [Fact]
        public void IfTheSelectorThrowsThenTheOutputShouldHaveThatException()
        {
            var a = 1.ToExceptional();
            var b = 2.ToExceptional();

            var e = new Exception("AAA");
            var r = Exceptional.Combine<int, int, int>(a, b, (x, y) => { throw e; });

            r.HasException.Should().BeTrue();
            r.Exception.Should().Be(e);

        }

        [Fact]
        public void LINQHappyPathShouldWork()
        {
            var xa = Exceptional.Success(1);
            var xb = Exceptional.Success(2);

            var xx =
                from a in xa
                from b in xb
                select a + b;

            xx.HasException.Should().BeFalse();
            xx.Value.Should().Be(3);



        }
        [Fact]
        public void LINQSadPathShouldWork()
        {
            var xa = Exceptional.Success(1);
            var xb = Exceptional.Failure<int>(new Exception("foo"));

            var xx =
                from a in xa
                from b in xb
                select a + b;

            xx.HasException.Should().BeTrue();
            xx.Exception.Message.Should().Be("foo");
            new Action
                (() =>
                {
                    var foo = xx.Value;
                }).ShouldThrow<Exception>();



        }

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
    }
}