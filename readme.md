Exceptional
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
