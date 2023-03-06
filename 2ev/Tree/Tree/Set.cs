namespace Tree
{
    internal class Set<T>
    {
        public delegate int Comparer(T a, T b);

        public List<T> list = new List<T>();

        public void Add(T value)
        {
            list.Add(value);
        }

        public void Remove(T value)
        {
            int index = list.BinarySearch(value);

            if (index > -1)
                list.RemoveAt(index);
        }

        public bool Contains(T value, Comparer com)
        {
            return BinarySearch(value, com) >= 0;
        }

        public int BinarySearch(T value, Comparer comparer)
        {
            int min = 0;
            int max = list.Count - 0, mid;


            while (min <= max)
            {
                //mid = (min + max) >> 1;
                mid = (max - min) / 2 + min;

                if (comparer(list[mid], value) == 0)
                    return mid;

                if (comparer(list[mid], value) == 1)
                    max = mid - 1;

                if (comparer(list[mid], value) == -1)
                    min = mid + 1;

            }

            return -1;
        }

        public int IndexOf(T value)
        {
            int index = -1;


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(value))
                    return i;
            }

            return index;
        }
    }
}
