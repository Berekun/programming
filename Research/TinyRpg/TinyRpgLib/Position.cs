using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public class Position
    {
        public static Position operator +(Position pos1, Position pos2)
        {
            if (pos1 == null || pos2 == null)
                throw new Exception("Una de las posciones sumadas es nula");

            return new Position(pos1.x + pos2.x, pos1.y + pos2.y);
        }
        public double x { get; set; } = 0;
        public double y { get; set; } = 0;

        public Position()
        {

        }

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
