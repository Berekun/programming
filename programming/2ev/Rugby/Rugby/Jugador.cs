using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public abstract class Jugador : Personaje
    {
        protected Equipo _equipo;
        protected int disbleturns = 0;

        public Equipo Equipo => _equipo;

        public Jugador(Equipo equipo, int x, int y) : base(x, y)
        {
            _equipo = equipo;
            _equipo.AddPlayers(this);
        }

        // Javi: No sería mejor una función a la que le pasaras un entero?
        public Position GetRandomPosition5x5(Partido partido)
        {
            List<Position> list = new List<Position>();

            for (int i = 2; i > -3; i--)
            {
                for (int j = -2; j < 3; j++)
                {
                    // Javi: ContainsPersonaje
                    if (!partido.IsPersonajeAt(X + j, Y + i))
                        list.Add(new Position(X + j, Y + i));
                }
            }

            return list[(int)Utils.GetRandomDouble(0, list.Count)];
        }

        public Position GetPositionOfBall3x3(Partido partido)
        {
            Position position = new Position();

            for (int i = 1; i > -2; i--)
            {
                for (int j = -1; j < 2; j++)
                {
                    // Javi: En este if no iria un break?
                    if (partido.GetPositionOfBallWithoutPlayer(j,i).x != 0)
                        position = (partido.GetPositionOfBallWithoutPlayer(j, i));
                }
            }
            return position;
        }

        // Javi: No sería mejor un parámetro en la funcion con el rango de casillas?
        public Position GetPositionOfPlayerWithBall3x3(Partido partido)
        {
            Position position = new Position();

            for (int i = 1; i > -2; i--)
            {
                for (int j = -1; j < 2; j++)
                {
                    // Javi: break?
                    if (partido.GetPositionPlayerWhitBall(j, i).x != 0)
                        position = (partido.GetPositionOfBallWithoutPlayer(j, i));
                }
            }
            return position;
        }

        public void DisableTurns(int turns)
        {
            disbleturns += turns;
        }
    }
}
