using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBTools_Core.Extensions {
    public static class IEnumerableExtensions {
        private const double goldenRatioReciprocal = 0.6180339887498948482045868343656381;

        public static T[] Shuffle<T>(this IEnumerable<T> array) => array.ToArray().Shuffle();

        /// <summary>
        /// Returns the index of the first element which matches the predicate. Returns -1 if none match
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> list, Func<T, bool> predicate) {
            int i = -1;
            foreach (T t in list) {
                i++;
                if (predicate(t))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// shuffle and stream a collection on the reciprocal golden ratio. Will evenly distribute ordered values. May miss values on very large arrays due to rounding errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="useRandomStart">Start at a random index</param>
        /// <returns></returns>
        public static IEnumerable<T> Interweave<T>(this IEnumerable<T> array, bool useRandomStart = true) {
            double randState = useRandomStart ? ArrayExtensions.defaultRand.NextDouble() : 0;
            var list = array.ToList();
            for (int i = list.Count - 1; i >= 0; i--) {
                randState = (randState + goldenRatioReciprocal) % 1.0;
                yield return list[(int)Math.Floor(randState * list.Count)];
            }
        }

        /// <summary>
        /// Orders a collection then interweaves it for an ideal spread of values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="keySelector">Order by</param>
        /// <param name="useRandomStart">Start at a random index</param>
        /// <returns></returns>
        public static IEnumerable<T> Balance<T>(this IEnumerable<T> array, Func<T, T> keySelector, bool useRandomStart = true) where T : IComparable<T>
            => array.OrderBy(keySelector).Interweave(useRandomStart);

        /// <summary>
        /// Orders a collection then interweaves it for an ideal spread of values. Ordered by element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="useRandomStart">Start at a random index</param>
        public static IEnumerable<T> Balance<T>(this IEnumerable<T> array, bool useRandomStart = true) where T : IComparable<T>
           => array.OrderBy(x => x).Interweave(useRandomStart);

        /// <summary>
        /// Distinct by a given selector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="list"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, K>(this IEnumerable<T> list, Func<T, K> selector) {
            var known = new HashSet<K>();
            foreach (T el in list) {
                if (known.Add(selector(el)))
                    yield return el;
            }
        }

        /// <summary>
        /// Returns the index of the last element which matches the predicate. Returns -1 if none match
        /// </summary>
        public static int LastIndexOf<T>(this IList<T> list, Func<T, bool> predicate) {
            for (int i = list.Count - 1; i >= 0; i++) {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the matching value if the given key exists. Otherwise returns the given default
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <typeparam name="Val"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Val GetValueOrElse<Key, Val>(this Dictionary<Key, Val> dic, Key key, Val defaultValue = default)
            => dic.TryGetValue(key, out Val value) ? value : defaultValue;

        /// <summary>
        /// Accepts a collection of tasks, and awaits them in order, returning the results when all are finished.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> AwaitSequentially<T>(this IEnumerable<Task<T>> tasks) {
            var results = new List<T>(tasks.Count());//Is it better to not check this and wait on lazy?
            foreach (Task<T> t in tasks) {
                results.Add(await t);
            }
            return results;
        }

        /// <summary>
        /// Accepts a collection of tasks, and starts all, returning them as a list when they all finish. The new order is the order in which tasks finished
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> AwaitWhenever<T>(this IEnumerable<Task<T>> tasks) => await Task.WhenAll(tasks);

        /// <summary>
        /// Return a random element from the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T Random<T>(this IEnumerable<T> array) {
            if (array.Any())
                return array.ElementAt(ArrayExtensions.defaultRand.Next(array.Count()));
            else
                throw new Exception("Sequence contains no elements");
        }

        /// <summary>
        /// Applies a function to each element in a collection sequentially and yields the results
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<TOut> Map<TIn, TOut>(this IEnumerable<TIn> list, Func<TIn, TOut> func) {
            if (list == null || func == null)
                throw new ArgumentNullException();

            return Map();

            IEnumerable<TOut> Map() {
                foreach (TIn t in list)
                    yield return func(t);
            }
        }

        /// <summary>
        /// Applies a void function (Action) to each element in a collection sequentially
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        public static void Map<T>(this IEnumerable<T> list, Action<T> func) {
            if (list == null || func == null)
                throw new ArgumentNullException();

            foreach (T t in list)
                func(t);
        }

        /// <summary>
        /// Applies and awaits an async function to each element in a collection sequentially, and returns their results in a new list
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TOut>> MapAsync<TIn, TOut>(this IEnumerable<TIn> list, Func<TIn, Task<TOut>> func) {
            if (list == null || func == null)
                throw new ArgumentNullException();

            var results = new List<TOut>();
            foreach (TIn t in list)
                results.Add(await func(t));
            return results;
        }

        /// <summary>
        /// Applies and awaits an asynct Task function (Action) to each element in a collection sequentially
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task MapAsync<TIn>(this IEnumerable<TIn> list, Func<TIn, Task> func) {
            if (list == null || func == null)
                throw new ArgumentNullException();

            foreach (TIn t in list)
                await func(t);
        }

        /// <summary>
        /// Applies an async function to each element and returns the results. The order of the new list is the order in which the tasks completed, NOT the order of the initial list
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TOut>> MapUnorderedAsync<TIn, TOut>(this IEnumerable<TIn> list, Func<TIn, Task<TOut>> func) {
            if (list == null || func == null)
                throw new ArgumentNullException();

            var tasks = new List<Task<TOut>>();
            foreach(TIn t in list) {
                tasks.Add(func(t));
            }

            var results = new List<TOut>(tasks.Count);
            Task<TOut> completed;
            while (tasks.Any()) {
                completed = await Task.WhenAny(tasks);
                results.Add(completed.Result);
                tasks.Remove(completed);
            }
            return results;
        }

        /// <summary>
        /// Applies an async Task function (Action) to each element. The tasks run in parallel, the original list order is NOT preserved
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task MapUnorderedAsync<TIn>(this IEnumerable<TIn> list, Func<TIn, Task> func) {
            if (list == null || func == null)
                throw new ArgumentNullException();

            var tasks = new List<Task>();
            foreach (TIn t in list) {
                tasks.Add(func(t));
            }
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Enumerates the list and applies the given action to each element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        [Obsolete("Use Map now, which has an overload of the same signature, for consistency")]
        public static void MapAction<T>(this IEnumerable<T> list, Action<T> action) {
            if (list == null || action == null)
                throw new ArgumentNullException();

            foreach (T item in list)
                action(item);
        }

        /// <summary>
        /// Console prints each item in the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="newLine"></param>
        public static void Print<T>(this IEnumerable<T> list, bool newLine = true) {
            if (newLine)
                list.Map(x => Console.WriteLine(x));
            else
                list.Map(x => Console.Write(x));
        }

        public static IEnumerable<T> ConcatMany<T>(this IEnumerable<T> list, params IEnumerable<T>[] lists) {
            foreach (T t in list)
                yield return t;

            foreach (IEnumerable<T> l in lists) {
                foreach (T t in l)
                    yield return t;
            }
        }

        /// <summary>
        /// Returns true if more than count objects are in the list. Short curcuits, so useful with large lists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool CountGreaterThan<T>(this IEnumerable<T> list, int count) {
            int i = 1;
            foreach (T t in list) {
                if (i > count)
                    return true;
                else
                    i++;
            }
            return false;
        }

        public static IEnumerable<string> PadToLongest(this IEnumerable<string> list, char paddingChar = ' ', bool padLeft = false) {
            int maxLength = list.Max(x => x.Length);
            foreach (string s in list) {
                yield return padLeft ? s.PadLeft(maxLength, paddingChar) : s.PadRight(maxLength, paddingChar);
            }
        }

        internal static IEnumerable<int> Init(this IEnumerable<int> list, int startValue = 0, bool increment = false) {
            _ = list.All(x => {
                x = startValue;
                startValue += increment ? 1 : 0;
                return true;
            });
            return list;
        }
    }
}
