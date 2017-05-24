using System;
using System.Collections.Generic;

namespace Weingartner
{
    public interface IExceptional<out T> 
    {
        bool HasException { get; }
        Exception Exception { get; }
        T Value { get; }
        string ToMessage();
        void ThrowIfHasException();
    }
}