using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    internal class Delantero : Jugador
    {
        public Delantero(Equipo equipo, int x, int y) : base(equipo, x, y)
        {

        }

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            if (pelota.Y == position.y && pelota.X == position.x)
            {
                if (Utils.GetRandomDouble(0, 1) < 0.6)
                {
                    Position moveposition = GetRandomPosition3x3(partido);
                    partido.Move(position, moveposition);
                    pelota.ChangePosition(partido, moveposition);

                    if (Utils.GetRandomDouble(0, 1) <= 0.2)
                    {
                        Ejecutar(pelota, partido);
                    }
                }
                else
                {
                    if (Utils.GetRandomDouble(0, 1) < probPas)
                    {
                        pelota.ChangePosition(partido, _equipo.GetPersonajeAt(Utils.GetRandomInt(0, _equipo.GetPersonajesCount())).Position);
                    }
                    else
                    {
                        pelota.ChangePosition(partido, GetRandomPosition5x5(partido));
                    }
                }
            }
            else
            {
                partido.Move(position,GetRandomPositionStrike(partido));
            }
        }

        //No esta hecho hazlo en casa chaval
        public Position GetRandomPositionStrike(Partido partido)
        {
            int scoreup = 0;
            int scoremid = 0;
            int scoredown = 0;

            for (int i = -1; i < 2; i++)
            {
                if (!partido.IsPersonajeAt(position.x + 1, position.y + i))
                    scoreup++;
            }
            for (int i = -1; i < 2; i++)
            {
                if (!partido.IsPersonajeAt(position.x + 1, position.y + i))
                    scoremid++;
            }
            for (int i = -1; i < 2; i++)
            {
                if (!partido.IsPersonajeAt(position.x + 1, position.y + i))
                    scoredown++;
            }

            List<int> list = new List<int>();
            list.Add(scoreup);
            list.Add(scoremid);
            list.Add(scoredown);

            list.Sort();
            //TO DO ordenar

            return position;
        }
    }
}
