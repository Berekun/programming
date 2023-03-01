using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    internal class GlamourCar : Car
    {
        public GlamourCar(string name, double position) : base(name, position, ObjectType.GLAMOURCAR)
        {
            drivers.Add(new Human("Rubia", RacersType.HUMAN));
        }

        public override void Simulate(IRace race)
        {
            Move(20);
        }
    }
}
