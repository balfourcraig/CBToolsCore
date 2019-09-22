using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.Collections
{
    public class BTreeOption<T> : IEnumerable<BTreeOption<T>>
    {
        public IOption<BTreeOption<T>> parent;
        public IOption<BTreeOption<T>> left;
        public IOption<BTreeOption<T>> right;
        public IOption<T> value;

        public BTreeOption()
        {
            this.parent = new None<BTreeOption<T>>();
            this.left = new None<BTreeOption<T>>();
            this.right = new None<BTreeOption<T>>();
            this.value = new None<T>();
        }

        public BTreeOption(IOption<BTreeOption<T>> parent, IOption<T> value): this(new None<BTreeOption<T>>(), new None<BTreeOption<T>>(), parent, value)
        {

        }

        public BTreeOption(IOption<BTreeOption<T>> left, IOption<BTreeOption<T>> right, IOption<BTreeOption<T>> parent, IOption<T> value)
        {
            this.parent = parent;
            this.left = left;
            this.right = right;
            this.value = value;
        }


        public bool IsRoot => !parent.HasValue(out _);
        public bool IsLeaf => !left.HasValue(out _) && !right.HasValue(out _);

        public IEnumerable<BTreeOption<T>> Decendents(bool fromRight = true)
        {
            yield return this;

            if((fromRight && right.HasValue(out BTreeOption<T> child)) || (!fromRight && left.HasValue(out child)))
                foreach (BTreeOption<T> xChild in child.Decendents(fromRight))
                    yield return xChild;
            
            if ((fromRight && left.HasValue(out child)) || (!fromRight && right.HasValue(out child)))
                foreach (BTreeOption<T> xChild in child.Decendents(fromRight))
                    yield return xChild;
            
        }

        public bool FindNodeByValue(T toFind, out BTreeOption<T>? node, bool fromRight = true)
        {
            if (value.Equals(toFind))
            {
                node = this;
                return true;
            }
            else
            {
                if (((fromRight && right.HasValue(out var child)) || (!fromRight && left.HasValue(out child))) && child.FindNodeByValue(toFind, out node, fromRight))
                    return true;
                else
                {
                    node = null;
                    return false;
                }
            }
        }

        public IEnumerator<BTreeOption<T>> GetEnumerator()
        {
            return Decendents(true).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
