namespace Weingartner.Exceptional
{

    using System;
    using System.Collections;
    using System.Collections.Generic;

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
            if (obj.GetType() != this.GetType())
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

        public bool HasException { get; private set; }
        public Exception Exception { get; private set; }
        private readonly T _Value = default(T);
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
            return this.HasException ? Exception.Message : "";
        }

        public override string ToString()
        {
            return (this.HasException ? Exception.GetType().Name : ((Value != null) ? Value.ToString() : "null"));
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

        public static IExceptional<T> Execute<U0, T>(Func<U0, T> fn, U0 u0)
        {
            return Exceptional.Execute(() => fn(u0));
        }

        public static IExceptional<T> Execute<U0, U1, T>(Func<U0, U1, T> fn, U0 u0, U1 u1)
        {
            return Exceptional.Execute(() => fn(u0, u1));
        }
        public static IExceptional<T> Execute<U0, U1, U2, T>(Func<U0, U1, U2, T> fn, U0 u0, U1 u1, U2 u2)
        {
            return Exceptional.Execute(() => fn(u0, u1, u2));
        }
        public static IExceptional<T> Execute<U0, U1, U2, U3, T>(Func<U0, U1, U2, U3, T> fn, U0 u0, U1 u1, U2 u2, U3 u3)
        {
            return Exceptional.Execute(() => fn(u0, u1, u2, u3));
        }
        public static IExceptional<T> Execute<U0, U1, U2, U3, U4, T>(Func<U0, U1, U2, U3, U4, T> fn, U0 u0, U1 u1, U2 u2, U3 u3, U4 u4)
        {
            return Exceptional.Execute(() => fn(u0, u1, u2, u3, u4));
        }
    }
}
