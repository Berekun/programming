using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    internal class Defensa : Jugador
    {
        protected double probPas = Utils.GetRandomDouble(0.2, 0.8);
        protected double probSteal = Utils.GetRandomDouble(0.4, 0.6);
        public Defensa(Equipo equipo, int x, int y) : base(equipo, x, y)
        {

        }

        // Javi: Esta función está muy bien hecha, pero permíteme unas mejoras estéticas
        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            if (disbleturns == 0)
            {
                // Javi: En vez de esto, llamaría a una función TengoLaPelota(pelota)
                if (pelota.Y == Y && pelota.X == X)
                {
                    // Javi: Los nombres de las funciones debería empezar por verbo
                    DefenseWithBall(pelota, partido);
                }
                else
                {
                    DefenseWithoutBall(pelota, partido);
                }
            }
            else
                disbleturns -= 1;
            
        }

        public void DefenseWithBall(Pelota pelota, Partido partido)
        {
            if (Utils.GetRandomDouble(0, 1) < 0.5)
            {
                Position moveposition = partido.GetRandomPosition3x3(partido, this);
                partido.MovePersonaje(this, moveposition);
                pelota.ChangePosition(moveposition);
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

        public void DefenseWithoutBall(Pelota pelota, Partido partido)
        {
            double random = Utils.GetRandomDouble(0, 1);
            if (random < 0.25)
            {
                // Javi: Tabula
                if(GetPositionOfBall3x3(partido).x != 0)
                partido.MovePersonaje(this, GetPositionOfBall3x3(partido));
            }
            else if (random > 0.25 && 0.5 > random)
            {
                if(Utils.GetRandomDouble(0,1) < probSteal)
                {
                    if (GetPositionOfPlayerWithBall3x3(partido).x != 0)
                        pelota.ChangePosition(GetRandomPosition5x5(partido));
                }
            }
            else if (random > 0.5 && 0.75 > random)
            {
                // Javi: Creo que la funcion hubiese sido mejor: this.AproximateToPlayer(partido)
                partido.AproxToPlayer(this);
            }
            else
            {
                partido.AproxToBall(this);
            }

        }
    }
}
