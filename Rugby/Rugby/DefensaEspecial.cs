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
            GetRandomPersonaje3x3(partido).SetDisable(1);
        }

        public Jugador GetRandomPersonaje3x3(Partido partido)
        {
            List<Jugador> list = new List<Jugador>();

            for (int i = -1; i < 2; i++)
            {
                if (partido.GetPersonajeAtPosition(X + i, Y + 1) != null)
                    list.Add((Jugador)partido.GetPersonajeAtPosition(X + i, Y + 1));
            }

            for (int i = -1; i < 2; i++)
            {
                if (partido.GetPersonajeAtPosition(X + i, Y) != null)
                    list.Add((Jugador)partido.GetPersonajeAtPosition(X + i, Y));
            }

            for (int i = -1; i < 2; i++)
            {
                if (partido.GetPersonajeAtPosition(X + i, Y - 1) != null)
                    list.Add((Jugador)partido.GetPersonajeAtPosition(X - i, Y - 1));
            }

            return list[Utils.GetRandomInt(0, list.Count)];
        }
    }
}
