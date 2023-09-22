using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public class Personaje
    {
        public Position position { get; set; }
        public int vida { get; set; } = 0;
        public double transparency { get; set; } = 1;
        public Personaje()
        {

        }
        public Personaje(double x, double y, int vida, double maxXValue, double maxYValue)
        {
            position = new Position(x, y, maxXValue, maxYValue);
            this.vida = vida;
        }
    }
}
