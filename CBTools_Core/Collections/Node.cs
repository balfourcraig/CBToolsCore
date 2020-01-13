using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CBTools_Core.Collections {
    public class Node<T> : IEnumerable<Node<T>> {
        public readonly int id;
        public T contents;
        public List<Node<T>> neighbors;

        public bool IsLeaf => neighbors.Count == 1;

        public Node(int id, T contents, params Node<T>[] neighboringNodes) {
            this.id = id;
            this.contents = contents;

            if (neighboringNodes != null && neighboringNodes.Length > 0)
                neighbors = neighboringNodes.ToList();
            else
                neighbors = new List<Node<T>>();
        }

        public IEnumerable<Node<T>> Connected {
            get {
                var q = new Queue<Node<T>>();
                var discovered = new HashSet<Node<T>>();

                q.Enqueue(this);
                discovered.Add(this);

                //List<Node<T>> nodes = new List<Node<T>>();

                while (q.Count > 0) {
                    Node<T> n = q.Dequeue();
                    yield return n;

                    foreach (Node<T> child in n.neighbors) {
                        if (!discovered.Contains(child)) {
                            discovered.Add(child);
                            q.Enqueue(child);
                        }
                    }
                }
            }
        }

        public bool BreadthFirstSearch(Predicate<Node<T>> predicate, out Node<T>? result) {
            var q = new Queue<Node<T>>();
            var discovered = new HashSet<Node<T>>();

            q.Enqueue(this);
            discovered.Add(this);

            while (q.Count > 0) {
                result = q.Dequeue();
                if (predicate(result)) {
                    return true;
                }

                foreach (Node<T> child in result.neighbors) {
                    if (!discovered.Contains(child)) {
                        discovered.Add(child);
                        q.Enqueue(child);
                    }
                }
            }
            result = null;
            return false;
        }

        public bool BreadthFirstPathSearch(Predicate<Node<T>> predicate, out List<Node<T>>? path) {
            var q = new Queue<Node<T>>();
            var discovered = new HashSet<Node<T>>();

            var parents = new Dictionary<Node<T>, Node<T>>();

            q.Enqueue(this);
            discovered.Add(this);

            while (q.Count > 0) {
                Node<T> n = q.Dequeue();
                if (predicate(n)) {
                    path = new List<Node<T>> { n };
                    while (parents.TryGetValue(n, out Node<T> parent)) {
                        n = parent;
                        path.Add(n);
                    }
                    return true;
                }

                foreach (Node<T> child in n.neighbors) {
                    if (!discovered.Contains(child)) {
                        discovered.Add(child);
                        parents.Add(child, n);
                        q.Enqueue(child);
                    }
                }
            }
            path = null;
            return false;
        }

        public IEnumerator<Node<T>> GetEnumerator() => Connected.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
