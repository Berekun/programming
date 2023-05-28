using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public class Time
    {
            static DateTime time1 = DateTime.Now;
            static DateTime time2 = DateTime.Now;

            public static float deltaTime;

            public static void UpdateDeltaTime()
            {
                time2 = DateTime.Now;
                deltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
                time1 = time2;
            }
    }
}
