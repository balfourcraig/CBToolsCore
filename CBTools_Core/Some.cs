using System;

namespace CBTools_Core {
#nullable disable
    public class Some<T> : IOption<T>, IEquatable<T>, IEquatable<Some<T>>, IEquatable<IOption<T>> {
        private readonly T value;

        public bool HasValue(out T value) {
            if (!this.value.Equals(default(T))) {
                value = this.value;
                return true;
            }
            else {
                value = default;
                return false;
            }
        }

        public T Unwrap() => value;

        public static bool operator ==(Some<T> x, Some<T> y) {
            return x.value.Equals(y.value);
        }

        public static bool operator ==(Some<T> x, IOption<T> y) {
            return y.HasValue(out T val) && x.value.Equals(val);
        }

        public static bool operator !=(Some<T> x, IOption<T> y) {
            if (y.HasValue(out T val)) {
                return !(x.value.Equals(val));
            }
            else {
                return true;
            }
        }
        public static bool operator !=(Some<T> x, Some<T> y) {
            return !(x == y);
        }

        public bool Equals(T other) => value.Equals(other);

        public bool Equals(Some<T> other) => value.Equals(other.value);

        public bool Equals(IOption<T> other) => other.HasValue(out T val) && value.Equals(val);

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            else if (obj is Some<T>)
                return value.Equals(((Some<T>)obj).value);
            else
                return false;
        }

        public override string ToString() => "Some<" + typeof(T) + "> (" + value.ToString() + ")";

        public override int GetHashCode() => value.GetHashCode();

        public T ValueOrDefault() => value;

        public Some(T value) {
            this.value = value;
        }
    }
}
