using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaMinasLib
{
    internal class Utils
    {
        public static Random random = new Random();

        public static int GetRandomInt(int min, int max)
        {
            if (min > max)
                return GetRandomInt(max, min);
            return random.Next(min,max);
        }
    }
}
