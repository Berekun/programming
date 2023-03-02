﻿using System;
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

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            if (pelota.Y == Y && pelota.X == X)
            {
                DefenseWithBall(pelota, partido);
            }
            else
            {
                partido.Move(position,GetRandomPosition3x3(partido));
            }
        }

        public void DefenseWithBall(Pelota pelota, Partido partido)
        {
            if (Utils.GetRandomDouble(0, 1) < 0.5)
            {
                Position moveposition = GetRandomPosition3x3(partido);
                partido.Move(position, moveposition);
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

        public void DefenseWithoutBall(Pelota pelota, Partido partido)
        {
            double random = Utils.GetRandomDouble(0, 1);
            if (random < 0.25)
            {
                if(GetPositionOfBall3x3(partido).x != 0)
                partido.Move(position, GetPositionOfBall3x3(partido));
            }
            else if (random > 0.25 && 0.5 > random)
            {
                if(Utils.GetRandomDouble(0,1) < probSteal)
                {
                    if (GetPositionOfPlayerWithBall3x3(partido).x != 0)
                        pelota.ChangePosition(partido, GetRandomPosition5x5(partido));
                }
            }
            else if (random > 0.5 && 0.75 > random)
            {
                partido.AproxToPlayer(this);
            }
            else
            {

            }

        }
    }
}