using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    internal class Defensa : Jugador
    {
        public Defensa(Equipo equipo, int x, int y) : base(equipo, x, y)
        {
        }

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            if (pelota.Y == Y && pelota.X == X)
            {
                if (Utils.GetRandomDouble(0, 1) < 0.5)
                {
                    Position moveposition = GetRandomPosition3x3(partido);
                    partido.Move(position,moveposition);
                    pelota.ChangePosition(partido, moveposition);
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
                partido.Move(position,GetRandomPosition3x3(partido));
            }
        }
    }
}
