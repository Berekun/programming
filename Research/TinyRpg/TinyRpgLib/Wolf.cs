using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    internal class Wolf : Enemigo
    {
        public Wolf(int x, int y, int pathingRoute, int vida) : base(x, y, pathingRoute, vida)
        {
        }
    }
}
