using System;

namespace CBTools_Core {
    public readonly struct OptionStruct<T> : IEquatable<OptionStruct<T>>, IEquatable<T>/*, IOption<T>*/  where T : struct {
        private readonly T value;
        private readonly bool hasVal;

        public bool HasValue(out T value) {
            if (hasVal)
                value = this.value;
            else
                value = default;
            return hasVal;
        }

        public T Unwrap() {
            if (hasVal)
                return value;
            else
                throw new NullReferenceException("OptionStruct<" + typeof(T) + "> has no value");
        }

        public OptionStruct<T> AsNoValue() => new OptionStruct<T>(false, value);

        public bool Equals(T other) => this.hasVal && value.Equals(other);

        public bool Equals(OptionStruct<T> other) => this.hasVal && other.HasValue(out T val) && value.Equals(val);

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            else
                return obj is OptionStruct<T> && ((OptionStruct<T>)obj).HasValue(out T val) && value.Equals(val);
        }

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => "OptionStruct<" + typeof(T) + ">: " + (hasVal ? "(" + value.ToString() + ")" : "no value");

        public T ValueOrDefault() => hasVal ? value : default;

        public OptionStruct(bool hasValue = false, T value = default) {
            this.hasVal = hasValue;
            this.value = value;
        }

        public OptionStruct(T value) {
            this.hasVal = false;
            this.value = value;
        }

        /// <summary>
        /// Converts to either Some or None
        /// </summary>
        /// <returns></returns>
        public IOption<T> Promote() {
            if (hasVal)
                return new Some<T>(value);
            else
                return new None<T>();
        }
    }
}
