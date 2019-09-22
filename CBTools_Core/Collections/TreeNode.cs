using System;
using System.Collections;
using System.Collections.Generic;

namespace CBTools_Core.Collections
{
    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {
        public readonly int id;
        public IOption<T> contents;
        public readonly TreeNode<T> parent;
        public List<TreeNode<T>> children;

        public TreeNode<T> Root => IsRoot ? this : parent.Root;
        public int Depth => IsRoot ? 0 : 1 + parent.Depth;
        public bool IsRoot => parent == null;
        public bool IsLeaf => children.Count == 0;
        public IEnumerable<TreeNode<T>> Descendents
        {
            get
            {
                yield return this;

                foreach (TreeNode<T> child in children)
                {
                    foreach (TreeNode<T> n in child.Descendents)
                        yield return n;
                }
            }
        }

        public IEnumerable<TreeNode<T>> Connected => Root.Descendents;

        public TreeNode(int id, T contents, TreeNode<T> parent)
        {
            this.id = id;
            if (contents == null)
                this.contents = new None<T>();
            else
                this.contents = new Some<T>(contents);

            this.parent = parent;
            if (parent != null)
            {
                parent.children.Add(this);
            }
            this.children = new List<TreeNode<T>>();
        }

        public bool BreadthFirstSearch(Predicate<TreeNode<T>> predicate, out TreeNode<T>? result)
        {
            Queue<TreeNode<T>> q = new Queue<TreeNode<T>>();
            HashSet<TreeNode<T>> discovered = new HashSet<TreeNode<T>>();

            q.Enqueue(this);
            discovered.Add(this);

            while (q.Count > 0)
            {
                result = q.Dequeue();
                if (predicate(result))
                    return true;

                foreach (TreeNode<T> child in result.children)
                {
                    if (!discovered.Contains(child))
                    {
                        discovered.Add(child);
                        q.Enqueue(child);
                    }
                }
            }
            result = null;
            return false;
        }

        public IOption<TreeNode<T>> BreadthFirstSearch(Predicate<TreeNode<T>> predicate)
        {
            Queue<TreeNode<T>> q = new Queue<TreeNode<T>>();
            HashSet<TreeNode<T>> discovered = new HashSet<TreeNode<T>>();

            q.Enqueue(this);
            discovered.Add(this);
            TreeNode<T> result;
            while (q.Count > 0)
            {
                result = q.Dequeue();
                if (predicate(result))
                    return new Some<TreeNode<T>>(result);

                foreach (TreeNode<T> child in result.children)
                {
                    if (!discovered.Contains(child))
                    {
                        discovered.Add(child);
                        q.Enqueue(child);
                    }
                }
            }
            return new None<TreeNode<T>>();
        }

        public bool BreadthFirstPathSearch(Predicate<TreeNode<T>> predicate, out List<TreeNode<T>>? path)
        {
            Queue<TreeNode<T>> q = new Queue<TreeNode<T>>();
            HashSet<TreeNode<T>> discovered = new HashSet<TreeNode<T>>();

            Dictionary<TreeNode<T>, TreeNode<T>> parents = new Dictionary<TreeNode<T>, TreeNode<T>>();

            q.Enqueue(this);
            discovered.Add(this);

            while (q.Count > 0)
            {
                TreeNode<T> n = q.Dequeue();
                if (predicate(n))
                {
                    path = new List<TreeNode<T>> { n };
                    while (parents.TryGetValue(n, out TreeNode<T> parent))
                    {
                        n = parent;
                        path.Add(n);
                    }
                    return true;
                }

                foreach (TreeNode<T> child in n.children)
                {
                    if (!discovered.Contains(child))
                    {
                        discovered.Add(child);
                        parents.Add(child, n);
                        q.Enqueue(child);
                    }
                }
            }
            path = null;
            return false;
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            return Descendents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
