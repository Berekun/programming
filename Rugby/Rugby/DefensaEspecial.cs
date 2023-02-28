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
            base.Ejecutar(pelota, partido);
            GetRandomPersonaje3x3(partido).DisableTurns(1);
        }

        public Jugador GetRandomPersonaje3x3(Partido partido)
        {
            List<Jugador> list = new List<Jugador>();

            for (int i = 1; i < -2; i--)
            {
                for(int j = -1; j < 2; j++)
                if (partido.GetPersonajeAtPosition(X + j, Y + i) != null)
                    list.Add((Jugador)partido.GetPersonajeAtPosition(X + j, Y + i));
            }

            return list[Utils.GetRandomInt(0, list.Count)];
        }
    }
}
