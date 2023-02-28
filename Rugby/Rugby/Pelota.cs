using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public class Pelota
    {
        private Position position = new Position();

        public int X => position.x;

        public int Y => position.y;

        public void ChangePosition(Partido partido, Position newPosition)
        {
            partido.Move(position, newPosition);
        }
    }
}
