using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public abstract class Jugador : Personaje
    {
        protected Equipo _equipo;
        protected int disbleturns = 0;
        protected int probPas = (int)Utils.GetRandomDouble(0.2,0.8);

        public Jugador(Equipo equipo, int x, int y) : base(x, y)
        {
            _equipo = equipo;
        }

        public Position GetRandomPosition3x3(Partido partido)
        {
            List<Position> list = new List<Position>();

            for (int i = -1; i < 2; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y + 1))
                    list.Add(new Position(X + i, Y + 1));
            }

            for (int i = -1; i < 2; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y))
                    list.Add(new Position(X + i, Y));
            }

            for (int i = -1; i < 2; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y - 1))
                    list.Add(new Position(X - i, Y - 1));
            }

            return list[(int)Utils.GetRandomDouble(0, list.Count)];
        }

        public Position GetRandomPosition5x5(Partido partido)
        {
            List<Position> list = new List<Position>();

            for (int i = -2; i < 3; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y + 2))
                    list.Add(new Position(X + i, Y + 2));
            }

            for (int i = -2; i < 3; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y + 1))
                    list.Add(new Position(X + i, Y + 1));
            }

            for (int i = -2; i < 3; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y))
                    list.Add(new Position(X + i, Y));
            }

            for (int i = -2; i < 3; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y - 1))
                    list.Add(new Position(X - i, Y - 1));
            }

            for (int i = -2; i < 3; i++)
            {
                if (!partido.IsPersonajeAt(X + i, Y - 2))
                    list.Add(new Position(X - i, Y - 2));
            }

                return list[(int)Utils.GetRandomDouble(0, list.Count)];
        }

        public void SetDisable(int turns)
        {
            disbleturns += turns;
        }
    }
}
