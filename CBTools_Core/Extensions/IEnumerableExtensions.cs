using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBTools_Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T[] Shuffle<T>(this IEnumerable<T> array) => array.ToArray().Shuffle();

        public static int IndexOf<T>(this IEnumerable<T> list, Func<T, bool> predicate) {
            int i = -1;
            foreach (T t in list) {
                i++;
                if (predicate(t))
                    return i;
            }
            return -1;
        }

        public static Val GetValueOrElse<Key,Val>(this Dictionary<Key,Val> dic, Key key, Val defaultValue) {
            if (dic.TryGetValue(key, out Val value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// Accepts a collection of tasks, and awaits them in order, returning the results when all are finished.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> AwaitSequentially<T>(this IEnumerable<Task<T>> tasks) {
            List<T> results = new List<T>(tasks.Count());//Is it better to not check this and wait on lazy?
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
        public static async Task<IEnumerable<T>> AwaitWhenever<T>(this IEnumerable<Task<T>> tasks) {
            List<Task<T>> asList = tasks.ToList();
            return await Task.WhenAll(asList);
        }

        public static T Random<T>(this IEnumerable<T> array)
        {
            if (array.Any())
                return array.ElementAt(ArrayExtensions.defaultRand.Next(array.Count()));
            else
                throw new Exception("Sequence contains no elements");
        }

        public static IEnumerable<T> Map<T>(this IEnumerable<T> list, Func<T, T> func)
        {
            if (list == null || func == null)
                throw new ArgumentNullException();

            return Map();

            IEnumerable<T> Map() {
                foreach (T t in list)
                    yield return func(t);
            }
        }

        public static IEnumerable<T> ConcatMany<T>(this IEnumerable<T> list, params IEnumerable<T>[] lists)
        {
            foreach (T t in list)
                yield return t;

            foreach (IEnumerable<T> l in lists)
                foreach (T t in l)
                    yield return t;
        }

        public static bool CountGreaterThan<T>(this IEnumerable<T> list, int count)
        {
            int i = 1;
            foreach (T t in list)
            {
                if (i > count) return true;
                else i++;
            }
            return false;
        }

        public static IEnumerable<string> PadToLongest(this IEnumerable<string> list, char paddingChar = ' ', bool padLeft = false)
        {
            int maxLength = list.Max(x => x.Length);
            foreach (string s in list)
            {
                if (padLeft)
                    yield return s.PadLeft(maxLength, paddingChar);

                else
                    yield return s.PadRight(maxLength, paddingChar);
            }
        }

        internal static IEnumerable<int> Init(this IEnumerable<int> list, int startValue = 0, bool increment = false)
        {
            list.All(x => {
                x = startValue;
                startValue += increment ? 1 : 0;
                return true;
            });
            return list;
        }
    }
}
