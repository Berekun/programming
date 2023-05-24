using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public enum EnemyType
    {
        GOLEM, WOLF, DARK_WIZZARD
    }

    public class Enemigo : Personaje
    {
        public int pathingRoute { get; set; } = 0;

        public Enemigo(int x, int y, int pathingRoute, int vida) : base(x, y)
        {
            position = new Position(x, y);
            this.pathingRoute = pathingRoute;
            this.vida = vida;
        }
    }
}
