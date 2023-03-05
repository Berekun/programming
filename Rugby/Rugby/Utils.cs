using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public delegate int Comparator<T>(T a, T b);
    internal class Utils
    {
        public static Random random = new Random();
        public static Position position = new Position();

        public static Comparator<int> comp = (a, b) =>
        {
            if (a > b)
                return 0;
            return 1;
        };

        public static double GetRandomDouble(double min, double max)
        {
            if (min > max)
                GetRandomDouble(max, min);
            return random.NextDouble() * (max - min) + min;
        }

        public static int GetRandomInt(int min, int max)
        {
            if (min > max)
                GetRandomInt(max, min);
            return random.Next(min,max);
        }

        public static List<T> Sort<T>(List<T> list, Comparator<T> comp)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count - 1; j++)
                {
                    if (comp(list[i], list[j]) == 0)
                    {
                        T aux = list[i];
                        list[i] = list[j];
                        list[j] = aux;
                    }
                }
            }
            return list;
        }
    }
}
