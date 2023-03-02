using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    internal class Human : Driver
    {
        public Human(string name, RacersType type) : base(name)
        {
            type = GetRacersTypes();
        }
        public override double GetVelocityExtra()
        {
            return 0.0;
        }

        public RacersType GetRacersTypes()
        {
            return RacersType.ANIMAL;
        }
    }
}
