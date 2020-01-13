using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CBTools_Core {
    public class LazyAsync<T> {
        private Lazy<Task<T>> instance;
        private readonly Func<Task<T>> factory;
        private readonly bool valueOnly;

        public LazyAsync(Func<Task<T>> factory) {
            this.factory = factory;
            instance = new Lazy<Task<T>>(() => Task.Run(factory), true);
            this.valueOnly = false;
        }

        public LazyAsync(T val) {
            this.factory = () => Task.FromResult(val);
            instance = new Lazy<Task<T>>(() => Task.Run(factory), true);
            this.valueOnly = true;
        }

        public TaskAwaiter<T> GetAwaiter() => instance.Value.GetAwaiter();

        public void Reset() {
            if (!valueOnly)
                instance = new Lazy<Task<T>>(() => Task.Run(factory), true);
        }
    }
}
