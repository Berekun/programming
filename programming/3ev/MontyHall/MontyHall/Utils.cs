using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    internal class Utils
    {
        private static Random _random = new Random();

        public static double GetDoubleRandom(double min, double max)
        {
            if (min > max)
                return GetDoubleRandom(max, min);

            return _random.NextDouble() * (max - min) + min;
        }

        public static int GetIntRandom(int min, int max)
        {
            if (min > max)
                return GetIntRandom(max, min);

            return _random.Next(min,max);
        }
    }
}
