using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    internal class Delantero : Jugador
    {
        protected int probPas = (int)Utils.GetRandomDouble(0.2, 0.4);
        public Delantero(Equipo equipo, int x, int y) : base(equipo, x, y)
        {

        }

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            if (disbleturns == 0)
            {
                if (pelota.Y == position.y && pelota.X == position.x)
                {
                    DelanteroWithBall(pelota, partido);
                }
                else
                {
                    DelanteroWithoutBall(pelota, partido);
                }
            }
            else
                disbleturns -= 1;

        }

        public void DelanteroWithBall(Pelota pelota, Partido partido)
        {
            List<Position> positions = new List<Position>();
            Position moveposition = position;
            if (Utils.GetRandomDouble(0, 1) < 0.6)
            {
                for (int i = -1; i < 2; i++)
                {
                    if(partido.GetPositionAt(position.x + i, position.y).x >= 0)
                    positions.Add(partido.GetPositionAt(position.x + i, position.y));
                }
                if (positions.Count == 0)
                    moveposition = position;
                else
                    moveposition = positions[Utils.GetRandomInt(0,positions.Count - 1)];

                partido.MovePersonaje(this, moveposition);
                pelota.ChangePosition(moveposition);
                
                if (Utils.GetRandomDouble(0, 1) <= 0.2)
                {
                    Ejecutar(pelota, partido);
                }
            }
            else
            {
                if (Utils.GetRandomDouble(0, 1) < probPas)
                {
                    pelota.ChangePosition(_equipo.GetPersonajeAt(Utils.GetRandomInt(0, _equipo.GetPersonajesCount())).Position);
                }
                else
                {
                    pelota.ChangePosition(GetRandomPosition5x5(partido));
                }
            }
        }

        public void DelanteroWithoutBall(Pelota pelota, Partido partido)
        {
            partido.MovePersonaje(this,GetRandomPositionStrike(partido));
        }

        public Position GetRandomPositionStrike(Partido partido)
        {
            List<Position> positionList = new List<Position>();

            int score = partido.GetScore(this);

            for (int i = -1; i < 2; i++)
            {
                if(score == 0 && partido.GetPositionAt(X + i, Y + 1).x >= 0)
                {
                    positionList.Add(partido.GetPositionAt(X + i, Y + 1));
                }
                else if (score == 1 && partido.GetPositionAt(X + i, Y).x >= 0)
                {
                    positionList.Add(partido.GetPositionAt(X + i, Y));
                }
                else if (score == 2 && partido.GetPositionAt(X + i, Y - 1).x >= 0)
                {
                    positionList.Add(partido.GetPositionAt(X + i, Y - 1));
                }
            }

            if (positionList.Count == 0)
                return position;
            return positionList[Utils.GetRandomInt(0, positionList.Count - 1)];
        }
    }
}
