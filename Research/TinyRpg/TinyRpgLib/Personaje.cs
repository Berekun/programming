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

        public Personaje()
        {

        }
        public Personaje(double x, double y)
        {
            position = new Position(x, y);
        }

        //Sprites Spites de animacions de personajes y de ataques
    }
}
