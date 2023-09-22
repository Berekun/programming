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
        public EnemyType enemyType { get; set; }  

        public Enemigo(double x, double y, int vida, EnemyType enemyType, double maxXValue, double maxYValue) : base(x, y, vida, maxXValue, maxYValue)
        {
            position = new Position(x, y, maxXValue, maxYValue);
            this.vida = vida;
            this.enemyType = enemyType;
        }
    }
}
