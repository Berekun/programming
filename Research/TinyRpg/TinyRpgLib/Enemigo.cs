using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public enum EnemyType
    {
        GOLEM, MINI_GOLEM, WOLF, DARK_WIZZARD
    }

    public class Enemigo : Personaje
    {
        public int pathingRoute { get; set; } = 0;
        public EnemyType enemyType { get; set; }  

        public Enemigo(double x, double y, int pathingRoute, int vida, EnemyType enemyType) : base(x, y, vida)
        {
            position = new Position(x, y);
            this.pathingRoute = pathingRoute;
            this.vida = vida;
            this.enemyType = enemyType;
        }
    }
}
