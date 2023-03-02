using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    internal class PiereCar : Car
    {
        public PiereCar(string name, double position) : base(name,position,ObjectType.PIERECAR)
        {
            drivers.Add(new Human("Piere", RacersType.HUMAN));
            drivers.Add(new Animal("PerroMalo", RacersType.ANIMAL));
        }

        public override void Simulate(IRace race)
        {

            if(Utils.GetRandom(0,1) <= 0.3)
            {
                
            }
            Move(18);
        }

        public List<RaceObject> WhatCarIsBehind(IRace race)
        {
            double carsPositions = 1000;
            List<RaceObject> car = new List<RaceObject>();

            for (int i = 0; i < race.GetObjectCount(); i++)
            {
                double diference = Math.Abs(Position - race.GetObjectAt(i).Position);
                if (diference < carsPositions && race.GetObjectAt(i).GetObjectType() is Car)
                {
                    carsPositions = diference;
                    car.Add(race.GetObjectAt(i));
                }
            }

            return car;
        }
    }
}
