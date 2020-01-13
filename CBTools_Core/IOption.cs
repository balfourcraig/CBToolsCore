namespace CBTools_Core {
#nullable disable
    public interface IOption<T> {
        /// <summary>
        /// Returns true if type is Some, or OptionStruct with type. Otherwise false. Value is in out parameter if exists
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool HasValue(out T value);

        /// <summary>
        /// Unwrap will return value if it exists, otherwise will throw an error (Err.ex if Err, otherwise null reference)
        /// </summary>
        /// <returns></returns>
        T Unwrap();

        /// <summary>
        /// Returns value if Some or OptionStruct with Type. Otherise will return default value (null for reference types)
        /// </summary>
        /// <returns></returns>
        T ValueOrDefault();
    }
}
