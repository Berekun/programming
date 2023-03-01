using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class TroglodyteCar : Car
    {
        public TroglodyteCar(string name, double position) : base(name, position, ObjectType.TROGLO)
        {
            drivers.Add(new Human("Tonto2"));
            drivers.Add(new Human("Tonto1"));
        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.TROGLO;
        }

        public override void Simulate(IRace race)
        {
            if (_disableturns == 0)
            {
                DisplacePlus(10);
                if (Utils.GetRandom(0, 1) <= 0.3)
                {
                    double dado = Utils.GetRandom(0, 1);
                    if (dado <= 0.4)
                        Displace(20);
                    if (dado <= 0.6)
                        Disable(1);
                }
            }
            Disable(-1);
        }
    }
}
