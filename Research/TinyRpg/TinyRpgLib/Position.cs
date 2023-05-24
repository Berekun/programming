using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDK;

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
        private double x;
        public double X
        {
            get => x;
            set
            {
                x = value;
                maxX = x + Constants.numberToCalculateMaxXorY;
            }
        }

        private double y;

        public double Y
        {
            get => y;
            set
            {
                y = value;
                maxY = y + Constants.numberToCalculateMaxXorY;
            }
        }
    
        public double maxX { get; set; }
        public double maxY { get; set; }

        public Position()
        {

        }

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
            maxX = x + 1;
            maxY = y + 1;
        }
    }
}
