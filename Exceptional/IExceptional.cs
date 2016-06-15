using System;
using System.Collections.Generic;

namespace Weingartner.Exceptional
{
    public interface IExceptional<out T> : IEnumerable<T>
    {
        bool HasException { get; }
        Exception Exception { get; }
        T Value { get; }
        string ToMessage();
        void ThrowIfHasException();
    }
}