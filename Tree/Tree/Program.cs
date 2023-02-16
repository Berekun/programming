using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    internal class TreeNode<T>
    {
        private List<TreeNode<T>> _children = new List<TreeNode<T>>();
        WeakReference<TreeNode<T>>? _parent;
        private T item;

        public delegate int Comparer(T a, T b);
        public delegate TreeNode<T> Visitor(TreeNode<T> node);
        public int ChildCount => _children.Count;


        public TreeNode(T item)
        {
            this.item = item;
        }

        public void AddChlidren(TreeNode<T> child)
        {
            if (child == null) return;

            child._parent = new WeakReference<TreeNode<T>>(this);
            _children.Add(child);

        }
        TreeNode<T>? GetParent()
        {
            if (_parent == null) return null;
            TreeNode<T>? result = null;
            _parent.TryGetTarget(out result);
            return result;
        }
        public void UnLink()
        {
            var p = GetParent();
            if (p == null) return;

            int index = p.GetChildIndex(this);
            p?._children.RemoveAt(index);
            p = null;
        }

        public void SetParent(TreeNode<T>? parent)
        {
            if (parent != null)
            {
                UnLink();
                parent.AddChlidren(this);
            }
        }
        public int GetChildIndex(TreeNode<T> node)
        {
            for (int i = 0; i < ChildCount; i++)
            {
                if (node == _children[i]) return i;
            }

            return -1;
        }

        public TreeNode<T> GetRoot()
        {
            var par = GetParent();
            if (par == null) return this;
            return par.GetRoot();
        }

        public bool Contains(T value, Comparer comp)
        {
            return FindNodeWithItem(value, comp) != null;
        }

        public TreeNode<T>? FindNodeWithItem(T value, Comparer compa)
        {
            if (value == null) return null;
            if (compa(value, this.item) == 1)
                return this;
            foreach (var child in _children)
            {
                var itemm = child.FindNodeWithItem(value, compa);
                if (itemm != null)
                    return itemm;
            }

            return null;
        }

        public void Visit(Visitor visitor)
        {
            visitor(this);
            foreach (var child in _children)
            {
                child.Visit(visitor);
            }
        }
    }
}