using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    internal class Pool<T> where T : new()
    {
        List<T> _list = new();

        public T Adquire()
        {
            if(_list.Count == 0)
                return new T();

            int index = _list.Count - 1;
            T item = _list[index];
            _list.RemoveAt(index);
            return item;
        }

        public void Release(T item)
        {
            _list.Add(item);
        }
    }
}
