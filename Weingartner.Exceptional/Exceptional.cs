using System;
using System.Collections.Generic;

namespace Weingartner
{
    // From http://stackoverflow.com/questions/10772727/exception-or-either-monad-in-c-sharp
    public class ExceptionalBase
    {
    }

    public static partial class Exceptional
    {
        [Obsolete("Use Fail")]
        public static IExceptional<T> Failure<T>(Exception e) => Fail<T>(e);
        [Obsolete("Use Ok")]
        public static IExceptional<T> Success<T>(T t) => Ok(t);

        public static IExceptional<T> Ok<T>(T t) => new Exceptional<T>(t);
        public static IExceptional<T> Fail<T>(Exception e) => new Exceptional<T>(e);
        public static IExceptional<T> Fail<T>(string msg) => Fail<T>(new Exception(msg)) ;

        public static IExceptional<T> Create<T>(Func<T> fn)
        {
            return new Exceptional<T>(fn);
        }

    }

    public class Exceptional<T> : ExceptionalBase, IExceptional<T>
    {
        protected bool Equals(Exceptional<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_Value, other._Value) && HasException == other.HasException && Equals(Exception, other.Exception);
        }

        public bool Equals(IExceptional<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (HasException != other.HasException)
                return false;

            if (HasException)
                return Equals(Exception, other.Exception);

            return EqualityComparer<T>.Default.Equals(_Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Exceptional<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<T>.Default.GetHashCode(_Value);
                hashCode = (hashCode*397) ^ HasException.GetHashCode();
                hashCode = (hashCode*397) ^ (Exception?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public static bool operator ==(Exceptional<T> left, IExceptional<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Exceptional<T> left, IExceptional<T> right)
        {
            return !Equals(left, right);
        }

        public bool HasException { get; }
        public Exception Exception { get; }
        private readonly T _Value;
        public T Value
        {
            get
            {
                if (HasException)
                {
                    throw Exception;
                }
                return _Value;
            }
        }

        public Exceptional(T value)
        {
            HasException = false;
            _Value = value;
        }

        public Exceptional(Exception exception)
        {
            HasException = true;
            Exception = exception;
        }

        public Exceptional(Func<T> getValue)
        {
            try
            {
                _Value = getValue();
                HasException = false;
            }
            catch (Exception exc)
            {
                Exception = exc;
                HasException = true;
            }
        }


        // Returns the exception message if it exists or an empty string
        public string ToMessage()
        {
            return HasException ? Exception.Message : "";
        }

        public override string ToString()
        {
            return (HasException ? Exception.GetType().Name : ((Value != null) ? Value.ToString() : "null"));
        }


        public void ThrowIfHasException()
        {
            if (HasException)
                throw Exception;
        }
    }

    public static partial class Exceptional
    {
        public static IExceptional<T> From<T>(T value)
        {
            return value.ToExceptional();
        }

        public static IExceptional<T> Execute<T>(Func<T> getValue)
        {
            return getValue.ToExceptional();
        }

        public static IExceptional<T> Execute<TU0, T>(Func<TU0, T> fn, TU0 u0)
        {
            return Execute(() => fn(u0));
        }

        public static IExceptional<T> Execute<TU0, TU1, T>(Func<TU0, TU1, T> fn, TU0 u0, TU1 u1)
        {
            return Execute(() => fn(u0, u1));
        }
        public static IExceptional<T> Execute<TU0, TU1, TU2, T>(Func<TU0, TU1, TU2, T> fn, TU0 u0, TU1 u1, TU2 u2)
        {
            return Execute(() => fn(u0, u1, u2));
        }
        public static IExceptional<T> Execute<TU0, TU1, TU2, TU3, T>(Func<TU0, TU1, TU2, TU3, T> fn, TU0 u0, TU1 u1, TU2 u2, TU3 u3)
        {
            return Execute(() => fn(u0, u1, u2, u3));
        }
        public static IExceptional<T> Execute<TU0, TU1, TU2, TU3, TU4, T>(Func<TU0, TU1, TU2, TU3, TU4, T> fn, TU0 u0, TU1 u1, TU2 u2, TU3 u3, TU4 u4)
        {
            return Execute(() => fn(u0, u1, u2, u3, u4));
        }
    }
}
