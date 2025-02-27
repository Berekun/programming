using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    internal class TroglodyteCar : Car
    {
        public TroglodyteCar(string name, double position) : base(name,position,ObjectType.TROGLODYTECAR)
        {
            drivers.Add(new Human("Troglodita1", RacersType.HUMAN));
            drivers.Add(new Human("Troglodita1", RacersType.HUMAN));
        }

        public override void Simulate(IRace race)
        {
            if(Utils.GetRandom(0,1) <= 0.3)
            {
                if (Utils.GetRandom(0, 1) <= 0.4)
                    Move(30);
                else if (Utils.GetRandom(0, 1) <= 0.2)
                    Move(0);
            }
            else
                Move(10);
        }
    }
}
