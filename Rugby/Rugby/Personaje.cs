using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public abstract class Personaje
    {
        protected Position position = new Position();

        public Position Position => position;
        public int X => position.x;

        public int Y => position.y;

        public Personaje(int x, int y)
        {
            position = new Position(x, y);
        }

        public void ChangePosition()
        {

        }

        public abstract void Ejecutar(Pelota pelota, Partido partido);
    }
}
