using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public class Bala
    {
        public Position position { get; set; }

        public int direction { get; set; }

        public Bala(double x, double y, int direction)
        {
            position.x = x;
            position.y = y;
            this.direction = direction;
        }
    }
}
