using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CBTools_Core {
    public class LazyAsync<T> {
        private Lazy<Task<T>> instance;
        private readonly Func<Task<T>> factory;

        public LazyAsync(Func<Task<T>> factory) {
            this.factory = factory;
            instance = new Lazy<Task<T>>(() => Task.Run(factory), true);
        }

        public LazyAsync(T val) {
            this.factory = () => Task.FromResult(val);
            instance = new Lazy<Task<T>>(() => Task.Run(factory), true);
        }

        public TaskAwaiter<T> GetAwaiter() {
            return instance.Value.GetAwaiter();
        }

        public void Reset() {
            instance = new Lazy<Task<T>>(() => Task.Run(factory), true);
        }
    }
}
