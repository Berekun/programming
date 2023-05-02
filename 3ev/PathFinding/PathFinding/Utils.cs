using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Utils
    {
        public static Random random = new Random();

        public static int GetRandom(int min, int max)
        {
            if(min > max)
                return random.Next(max, min);

            return random.Next(min, max);
        }
    }
}
