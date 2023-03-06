using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    internal class DefensaEspecial : Defensa
    {
        public DefensaEspecial(Equipo equipo, int x, int y) : base(equipo, x, y)
        {

        }

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            if (disbleturns == 0)
            {
                Jugador personaje = partido.GetRandomPersonaje3x3(partido, this);
                base.Ejecutar(pelota, partido);
                if (personaje != null)
                    personaje.DisableTurns(1);
            }
            else
                disbleturns -= 1;
        }
    }
}
