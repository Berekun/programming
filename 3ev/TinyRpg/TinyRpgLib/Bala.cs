using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDK;

namespace TinyRpgLib
{
    public enum Shooter
    {
        MAIN, ENEMIE
    }

    public class Bala
    {
        public Position position { get; set; }

        public vec2d_f64 direction { get; set; }

        public Shooter shooter { get; set; }

        public bool isHidden { get; set; }

        public Bala(double x, double y, vec2d_f64 direction, Shooter shooter)
        {
            position = new Position(x, y, 0.25,0.25);
            this.direction = direction;
            this.shooter = shooter;
        }
    }
}
