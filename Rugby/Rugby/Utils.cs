using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    internal class Utils
    {
        public static Random random = new Random();
        public static Position position = new Position();

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
    }
}
