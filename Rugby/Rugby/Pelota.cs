using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public class Pelota
    {
        private Position position = new Position(5,11);

        public Position Position => position;

        public int X => position.x;

        public int Y => position.y;

        public void ChangePosition(Position newPosition)
        {
            position = newPosition;
        }
    }
}
