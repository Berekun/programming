using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    internal class WoodCar : Car
    {
        public WoodCar(string name,double position) : base(name, position,ObjectType.WOODCAR)
        {
            drivers.Add(new Human("Carpintero", RacersType.HUMAN));
            drivers.Add(new Animal("Castor", RacersType.ANIMAL));
        }

        public override void Simulate(IRace race)
        {
            if (Utils.GetRandom(0, 1) <= 0.6)
            {

            }
            
            MoveComp(15);
        }

        public override void MoveComp(double position)
        {
            double v = position;
            foreach (var d in drivers)
            {
                v = d.GetVelocityExtra();
            }

            base.MoveComp(v);
        }
    }
}
