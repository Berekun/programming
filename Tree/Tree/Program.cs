using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    internal class TreeNode<T>
    {
        //Attrib
        private WeakReference<TreeNode<T>>? _parent;
        private List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public T item;

        //Properties
        public int ChildCount => _children.Count;

        public TreeNode<T>? parent => GetParent();

        //Construc
        public TreeNode()
        {
        }
        public TreeNode(T item)
        {
            this.item = item;
        }

        public TreeNode<T>? GetParent()
        {
            if (_parent == null) return null;
            TreeNode<T>? result = null;
            _parent.TryGetTarget(out result);
            return result;
        }

        public void AddChild(TreeNode<T>? child)
        {
            if (child == null) return;

            child._parent = new WeakReference<TreeNode<T>>(this);
            _children.Add(child);
        }

        public void SetParent(TreeNode<T>? newParent)
        {
            if (newParent == null) return;
            Unlink();
            newParent.AddChild(this);
        }

        public int GetIndexOf(TreeNode<T>? value)
        {
            if(value == null) return -1;

            for (int i = 0; i < ChildCount; i++)
            {
                if (_children[i] == value)
                    return i;
            }
            return -1;
        }

        public void Unlink()
        {
            TreeNode<T>? p = GetParent();
            if (p == null) return;

            int index = p.GetIndexOf(this);
            p._children.RemoveAt(index);
            p = null;
        }

        public TreeNode<T> GetRoot()
        {
            var p = GetParent();
            if (p == null) return this;
            return p.GetRoot();
        }




    }
}
