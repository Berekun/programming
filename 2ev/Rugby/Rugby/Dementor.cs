using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public class Dementor : Personaje
    {
        public Dementor(int x, int y) : base(x, y)
        {
        }

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            double random = Utils.GetRandomDouble(0, 1);
            Position moveposition = partido.GetRandomPosition3x3(partido, this);
            Jugador jugador;
            if (random <= 0.33)
            {
                jugador = partido.MostNearPlayer5x5(this);
                if (jugador != null)
                    jugador.DisableTurns(2);
            }
            else if (random > 0.33 && 0.66 <= random)
            {
                jugador = partido.GetRandomPersonaje3x3(partido, this);
                if(jugador != null)
                    jugador.DisableTurns(2);
            }
            else if (random > 0.66)
            {
                partido.MovePersonaje(this, moveposition);
            }

            if (Utils.GetRandomDouble(0, 1) < 0.2)
                Ejecutar(pelota, partido);
        }

    }
}
