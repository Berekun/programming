using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class WoodCar : Car
    {
        public WoodCar(string name, double position) : base(name, position, ObjectType.WOOD)
        {
            drivers.Add(new Human("Antonio"));
            drivers.Add(new Animal("Castor"));
        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.WOOD;
        }

        public override void Simulate(IRace race)
        {
            if (_disableturns != 0)
            {
                if (Utils.GetRandom(0, 1) <= 0.6)
                {
                    Disable(-_disableturns);
                }
            }
            DisplacePlus(15);
        }
    }
}
