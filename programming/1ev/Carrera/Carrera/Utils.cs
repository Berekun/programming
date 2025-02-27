using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Carrera
{
    internal class Utils
    {
        private static Random random = new Random();
        
        public static double Getrandom(int min,int max)
        {
            double r = random.NextDouble();
            double dis = max - min;
            return r * dis + min;
        }

        public static RunnerType Getrandomtype()
        {
            int min = 1; int max = 4;
            double r = random.NextDouble();
            double dis = max - min;
            double randomtype = r * dis + min;

            if (randomtype >= 1 && 2 > randomtype)
                return RunnerType.Marathon;
            if (randomtype >= 2 && 3 > randomtype)
                return RunnerType.Speedster;
            if (randomtype >= 3 && 4 > randomtype)
                return RunnerType.Thief;

            return 0;
        }
    }
}
