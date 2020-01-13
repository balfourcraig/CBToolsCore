using CBTools_Core.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CBTools_Core.Collections {
#nullable disable
    public class Trie<T> : IEnumerable<Trie<T>> {
        public readonly bool caseSensitive;
        public readonly char? letter;

        public readonly Dictionary<char, Trie<T>> children;
        public readonly Trie<T> parent;
        public readonly int depth;
        public bool completesString;
        public T value;

        public bool IsLeaf => children.Count == 0;
        public bool IsRoot => parent == null;
        public string Word {
            get {
                Span<char> word = new char[depth];
                return BuildWord(ref word);
            }
        }

        private string BuildWord(ref Span<char> word) {
            if (parent == null) {
                return word.ToString();
            }
            else {
                if (letter.HasValue)
                    word[depth - 1] = letter.Value;
                return parent.BuildWord(ref word);
            }
        }

        public Trie(bool caseSensitive = false) {
            this.letter = null;
            this.depth = 0;
            this.parent = null;
            this.children = new Dictionary<char, Trie<T>>();
            this.completesString = false;
            this.value = default;

            this.caseSensitive = caseSensitive;
        }

        internal Trie(char letter, Trie<T> parent) {
            this.parent = parent;
            this.depth = parent.depth + 1;
            this.children = new Dictionary<char, Trie<T>>();
            this.completesString = false;
            this.value = default;
            this.caseSensitive = (IsRoot ? false : parent.caseSensitive);
            this.letter = caseSensitive ? letter : char.ToUpperInvariant(letter);
        }

        /// <summary>
        /// Get the valide words with the prefix at this point in a breadth-first order.
        /// </summary>
        /// <param name="maxWords">Total words to return. Being breadth-first, shorter words are returned first.</param>
        /// <returns></returns>
        public IEnumerable<Trie<T>> BreadthFirstValidWords(int maxWords = int.MaxValue) {
            var q = new Queue<Trie<T>>();
            var discovered = new HashSet<Trie<T>>();

            q.Enqueue(this);
            _ = discovered.Add(this);

            Trie<T> current;
            while (q.Count > 0 && maxWords > 0) {
                current = q.Dequeue();
                foreach (Trie<T> child in current.children.Values) {
                    if (!discovered.Contains(child)) {
                        _ = discovered.Add(child);
                        q.Enqueue(child);
                        if (child.completesString) {
                            maxWords--;
                            yield return child;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Insert a new word associated with a value into the trie.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="value"></param>
        public void Add(ReadOnlySpan<char> word, T value) {
            if (word.Length == 0) {
                this.completesString = true;
                this.value = value;
            }
            else {
                char letter = caseSensitive ? word[0] : char.ToUpperInvariant(word[0]);
                if (!children.ContainsKey(letter)) {
                    children.Add(letter, new Trie<T>(letter, this));
                }
                children[letter].Add(word.Slice(1), value);
            }
        }

        public void Add(ReadOnlySpan<char> word, T value, out Trie<T> addedNode) {
            if (word.Length == 0) {
                addedNode = this;
                this.completesString = true;
                this.value = value;
            }
            else {
                char letter = caseSensitive ? word[0] : char.ToUpperInvariant(word[0]);
                if (!children.ContainsKey(letter)) {
                    children.Add(letter, new Trie<T>(letter, this));
                }
                children[letter].Add(word.Slice(1), value, out addedNode);
            }
        }

        public void Remove(ReadOnlySpan<char> word) {
            Trie<T> t = TraverseTo(word);
            if (t != null) {
                t.completesString = false;
                t.value = default;
                while (t.children.Count == 0 && !t.completesString) {
                    if (t.letter.HasValue)
                        _ = t.parent.children.Remove(t.letter.Value);
                    t = t.parent;
                }
            }
        }

        //This is basically a wrapper over TraverseTo to avoid nulls
        public bool TryGetValue(ReadOnlySpan<char> prefix, out T value) {
            Trie<T> node = TraverseTo(prefix);
            if (node != null && node.completesString) {
                value = node.value;
                return true;
            }
            else {
                value = default;
                return false;
            }
        }

        public int MaxDepth(ReadOnlySpan<char> prefix) {
            return prefix.Length == 1
                ? this.children.ContainsKey(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0])) ? 1 : 0
                : this.children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out Trie<T> set)
                    ? 1 + set.MaxDepth(prefix.Slice(1))
                    : 0;
        }

        public Trie<T> TraverseOrAdd(ReadOnlySpan<char> prefix, T valueIfAdding = default) {
            if (prefix.Length == 0) {
                return this;
            }
            else if (!IsLeaf && children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out Trie<T> n)) {
                return n.TraverseOrAdd(prefix.Slice(1));
            }
            else {
                Add(prefix.Slice(1), valueIfAdding, out Trie<T> added);
                return added;
            }
        }

        /// <summary>
        /// Step down to a given prefix. Usually this will be called from root.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public Trie<T> TraverseTo(ReadOnlySpan<char> prefix) {
            return prefix.Length == 0
                ? (this)
                : IsLeaf
                ? null
                : children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out Trie<T> n)
                ? n.TraverseTo(prefix.Slice(1))
                : null;
        }

        //Simple check. If you need to do anything with the value use TryGetValue to avoid double traversing
        public bool Contains(ReadOnlySpan<char> prefix) {
            return prefix.Length == 0
                ? completesString
                : IsLeaf
                ? false
                : children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out Trie<T> next)
                ? next.Contains(prefix.Slice(1))
                : false;
        }

        //This and Print are just for making the console display a pretty tree. It may safely be ignored.
        private static readonly ConsoleColor[] colors =
        {
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Green,
            ConsoleColor.Red
        };

        private static readonly ConsoleColor[] darkColors =
        {
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkYellow,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkRed
        };

        public void Print(int depth = 0) {
            int c = 0;
            if (depth > 0) {
                for (int i = 0; i < depth; i++) {

                    while (c > colors.Length - 1)
                        c -= colors.Length;
                    ConsoleWrite.WriteColored(darkColors[c], "   |");
                    c++;
                }
            }

            while (c > colors.Length - 1)
                c -= colors.Length;

            ConsoleWrite.WriteLinesColored(this.completesString ? colors[c] : darkColors[c], (IsLeaf ? " " : " > ") + this.letter);

            foreach (Trie<T> child in children.Values.OrderBy(x => x.children.Count)) {
                child.Print(depth + 1);
            }
        }

        public IEnumerator<Trie<T>> GetEnumerator() => BreadthFirstValidWords().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
