
using CBTools_Core.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CBTools_Core.Collections {
    public class TrieSet : IEnumerable<string> {
        public readonly bool caseSensitive;
        public readonly char letter;
        public readonly Dictionary<char, TrieSet> children;
        public readonly TrieSet? parent;
        public readonly int depth;
        public bool completesString;

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
                word[depth - 1] = letter;
                return parent.BuildWord(ref word);
            }
        }

        public TrieSet(bool caseSensitive = false) {
            this.letter = '\0';
            this.depth = 0;
            this.parent = null;
            this.children = new Dictionary<char, TrieSet>();
            this.completesString = false;
            this.caseSensitive = caseSensitive;
        }

        internal TrieSet(char letter, TrieSet parent) {
            this.parent = parent;
            this.depth = parent.depth + 1;
            this.children = new Dictionary<char, TrieSet>();
            this.completesString = false;
            this.caseSensitive = (parent == null ? false : parent.caseSensitive);
            this.letter = caseSensitive ? letter : char.ToUpperInvariant(letter);
        }

        public IEnumerable<string> BreadthFirstValidWords() => BreadthFirstValidWords(int.MaxValue);

        public IEnumerable<string> BreadthFirstValidWords(int maxWords) {
            var q = new Queue<TrieSet>();
            var discovered = new HashSet<TrieSet>();

            q.Enqueue(this);
            discovered.Add(this);

            TrieSet current;
            while (q.Count > 0 && maxWords > 0) {
                current = q.Dequeue();
                foreach (TrieSet child in current.children.Values) {
                    if (!discovered.Contains(child)) {
                        discovered.Add(child);
                        q.Enqueue(child);
                        if (child.completesString) {
                            maxWords--;
                            yield return child.Word;
                        }
                    }
                }
            }
        }

        public int MaxDepth(ReadOnlySpan<char> prefix) {
            if (prefix.Length == 1) {
                if (this.children.ContainsKey(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0])))
                    return 1;
                else
                    return 0;
            }
            else {
                if (this.children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out TrieSet set))
                    return 1 + set.MaxDepth(prefix.Slice(1));
                else
                    return 0;
            }
        }

        public void Add(ReadOnlySpan<char> word) {
            if (word.Length == 0) {
                this.completesString = true;
            }
            else {
                char letter = caseSensitive ? word[0] : char.ToUpperInvariant(word[0]);
                if (!children.ContainsKey(letter)) {
                    children.Add(letter, new TrieSet(letter, this));
                }
                children[letter].Add(word.Slice(1));
            }
        }

        public void Remove(ReadOnlySpan<char> word) {
            TrieSet? t = TraverseTo(word);
            if (t != null) {
                t.completesString = false;

                while (t.children.Count == 0 && !t.completesString && t.parent != null) {
                    t.parent.children.Remove(t.letter);
                    t = t.parent;
                }
            }
        }

        public TrieSet? TraverseTo(ReadOnlySpan<char> prefix) {
            if (prefix.Length == 0)
                return this;

            else if (IsLeaf)
                return null;

            else if (children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out TrieSet n))
                return n.TraverseTo(prefix.Slice(1));
            else
                return null;
        }

        public bool Contains(ReadOnlySpan<char> prefix) {
            if (prefix.Length == 0)
                return completesString;
            else if (IsLeaf)
                return false;
            else if (children.TryGetValue(caseSensitive ? prefix[0] : char.ToUpperInvariant(prefix[0]), out TrieSet next))
                return next.Contains(prefix.Slice(1));
            else return false;
        }

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

            foreach (TrieSet child in children.Values.OrderBy(x => x.children.Count)) {
                child.Print(depth + 1);
            }
        }

        public IEnumerator<string> GetEnumerator() => BreadthFirstValidWords().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
