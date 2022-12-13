namespace Listas
{
    internal class Program
    {
        public static int GetMajor(List<int> list)
        {
            int major = list[0];
            if(list == null)
                return 0;
            if(list.Count == 0)
                return 0;
            for(int i = 1; i < list.Count; i++)
            {
                if (list[i] > major)
                    major = list[i];
            }

            return major;
        }

        public static int IndexOf(List<int> list, int number)
        {
            int position = -1;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i] == number)
                    position = i;
            }

            return position;
        }

        public static bool IsNumberInList(List<int> list, int number)
        {
            return IndexOf(list, number) >= 0;
        }

        public static List<int> Sort(List<int> list)
        {
            int minor = int.MaxValue;
            int position = 0;
            int i = 0;
            List<int> result = new List<int>();

            while(list.Count > 0)
            {
                if (list[i] < minor)
                {
                    minor = list[i];
                    position = i;
                }
                if (i == list.Count - 1)
                {
                    result.Add(minor);
                    list.RemoveAt(position);
                    i = 0;
                    minor = int.MaxValue;
                    continue;
                }
                    
                i++;
            }
            return result;

        }
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(3);
            list.Add(5);
            list.Add(-1);
            list.Add(0);
            list.Add(7);
            list.Add(-4);
            list.Add(4);
            list.Add(-2);

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.Add(5);
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);

            var ll = Sort(list);
            list = list;
        }
    }
}