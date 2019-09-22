using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core
{
#nullable disable
    public readonly struct None<T> : IOption<T>, IEquatable<T>, IEquatable<None<T>>, IEquatable<IOption<T>>
    {
        public bool Equals(T other) => false;

        public bool Equals(None<T> other) => true;

        public bool Equals(IOption<T> other)
        {
            switch (other)
            {
                case None<T> _:
                    return true;
                default:
                    return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is None<T>)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            T val = default;
            return val.GetHashCode();
        }

        public static bool operator ==(None<T> _, IOption<T> y) => y is None<T>;
        public static bool operator !=(None<T> _, IOption<T> y) => !(y is None<T>);

        public bool HasValue(out T value)
        {
            value = default;
            return false;
        }

        public override string ToString() => "None<" + typeof(T) + ">";

        public T Unwrap()
        {
            throw new NullReferenceException();
        }

        public T ValueOrDefault() => default;
    }
}
