using System;

namespace CBTools_Core {
#nullable disable
    class Err<T> : IOption<T>, IEquatable<T>, IEquatable<Err<T>>, IEquatable<IOption<T>> {
        public readonly Exception ex;

        public string Message => ex.Message;


        public bool HasValue(out T value) {
            value = default;
            return false;
        }

        public void Throw() => throw ex;

        public T Unwrap() => throw ex;

        public bool Equals(T other) => false;

        public bool Equals(Err<T> other) => false;

        public bool Equals(IOption<T> other) => false;

        public override bool Equals(object obj) => false;

        public override int GetHashCode() => 0.GetHashCode();

        public Err(Exception exception) {
            this.ex = exception;
        }

        public Err(string message) {
            this.ex = new Exception(message);
        }

        public Err(string message, Exception innerException) {
            this.ex = new Exception(message, innerException);
        }

        public override string ToString() => "Err<" + typeof(T) + "> contains " + ex.Message;

        public T ValueOrDefault() => default;
    }
}
