using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CBTools_Core.Collections {
    public class TwinDictionary<X, Y> : IDictionary<X, Y> {
        private readonly Dictionary<X, Y> forwards;
        private readonly Dictionary<Y, X> backwards;

        private const string typeError = "Types of TwinDictionary cannot be the same, or indexing won't know which direction to index from";

        public IEnumerable<X> ForwardKeys => forwards.Keys;
        public IEnumerable<Y> BackwardsKeys => backwards.Keys;

        public IReadOnlyDictionary<X, Y> Forwards => forwards.ToImmutableDictionary(forwards.Comparer, backwards.Comparer);
        public IReadOnlyDictionary<Y, X> Backwards => backwards.ToImmutableDictionary(backwards.Comparer, forwards.Comparer);

        public ICollection<X> Keys => forwards.Keys;

        public ICollection<Y> Values => backwards.Keys;

        public int Count => forwards.Count;

        public bool IsReadOnly => false;

        public Y this[X key] {
            get => forwards[key];
            set {
                _ = backwards.Remove(value);
                backwards.Add(value, key);
                forwards[key] = value;
            }
        }
        public X this[Y key] {
            get => backwards[key];
            set {
                _ = forwards.Remove(value);
                forwards.Add(value, key);
                backwards[key] = value;
            }
        }

        public TwinDictionary() {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>();
            backwards = new Dictionary<Y, X>();
        }

        public TwinDictionary(int capacity) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>(capacity);
            backwards = new Dictionary<Y, X>(capacity);
        }

        public TwinDictionary(IDictionary<X, Y> collection) {
            forwards = new Dictionary<X, Y>(collection);
            backwards = new Dictionary<Y, X>(collection.Count);
            foreach (KeyValuePair<X, Y> pair in collection)
                backwards.Add(pair.Value, pair.Key);
        }

        public TwinDictionary(IDictionary<Y, X> collection) {
            backwards = new Dictionary<Y, X>(collection);
            forwards = new Dictionary<X, Y>(collection.Count);
            foreach (KeyValuePair<Y, X> pair in collection)
                forwards.Add(pair.Value, pair.Key);
        }

        public TwinDictionary(IEqualityComparer<X> forwardsComparer) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>(forwardsComparer);
            backwards = new Dictionary<Y, X>();
        }

        public TwinDictionary(IEqualityComparer<Y> backwardsComparer) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>();
            backwards = new Dictionary<Y, X>(backwardsComparer);
        }

        public TwinDictionary(IEqualityComparer<X> forwardsComparer, IEqualityComparer<Y> backwardsComparer) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>(forwardsComparer);
            backwards = new Dictionary<Y, X>(backwardsComparer);
        }

        public TwinDictionary(int capacity, IEqualityComparer<X> forwardsComparer) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>(capacity, forwardsComparer);
            backwards = new Dictionary<Y, X>(capacity);
        }

        public TwinDictionary(int capacity, IEqualityComparer<Y> backwardsComparer) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>(capacity);
            backwards = new Dictionary<Y, X>(capacity, backwardsComparer);
        }

        public TwinDictionary(int capacity, IEqualityComparer<X> forwardsComparer, IEqualityComparer<Y> backwardsComparer) {
            if (typeof(X) == typeof(Y))
                throw new ArgumentException(typeError);
            forwards = new Dictionary<X, Y>(capacity, forwardsComparer);
            backwards = new Dictionary<Y, X>(capacity, backwardsComparer);
        }

        public void Add(X key, Y value) {
            forwards.Add(key, value);
            backwards.Add(value, key);
        }
        public void Add(Y key, X value) => Add(value, key);

        public bool Remove(X key) => forwards.TryGetValue(key, out Y val) ? backwards.Remove(val) && forwards.Remove(key) : false;
        public bool Remove(Y key) => backwards.TryGetValue(key, out X val) ? forwards.Remove(val) && backwards.Remove(key) : false;

        public bool TryGetValue(X item1, out Y item2) => forwards.TryGetValue(item1, out item2);
        public bool TryGetValue(Y item1, out X item2) => backwards.TryGetValue(item1, out item2);

        public bool ContainsKey(X key) => forwards.ContainsKey(key);

        public void Clear() {
            forwards.Clear();
            backwards.Clear();
        }

        public bool ContainsKey(Y key) => backwards.ContainsKey(key);

        public void Add(KeyValuePair<X, Y> item) => Add(item.Key, item.Value);


        public bool Contains(KeyValuePair<X, Y> item) => forwards.Contains(item);

        /// <summary>
        /// Note: arrayIndex is unnused. Method only partially implemented
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<X, Y>[] array, int arrayIndex) {
            array = new KeyValuePair<X, Y>[forwards.Count];
            int i = 0;
            foreach (KeyValuePair<X, Y> item in forwards) {
                array[i++] = item;
            }
        }

        /// <summary>
        /// Not implemented. DO NOT USE!
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<X, Y> item) => throw new NotImplementedException();

        public IEnumerator<KeyValuePair<X, Y>> GetEnumerator() => forwards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
