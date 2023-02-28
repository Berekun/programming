using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    public class Utils
    {
        private static Random _random = new Random();

        public Random random => _random;
        public static double GetRandom(double min, double max)
        {
            if(min > max)
                return GetRandom(max, min);

            return _random.NextDouble() * (max - min) + min;

        }
    }
}
