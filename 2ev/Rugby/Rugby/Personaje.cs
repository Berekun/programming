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
        protected Position posInicial;

        public Position Position => position;

        public Position PositionInicial => posInicial;
        public int X => position.x;

        public int Y => position.y;

        public Personaje(int x, int y)
        {
            position = new Position(x, y);
            posInicial = position;
        }

        public void ChangePosition(Position newPosition)
        {
            position = newPosition;
        }

        public abstract void Ejecutar(Pelota pelota, Partido partido);
    }
}
