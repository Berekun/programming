using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class Animal : Driver
    {
        public Animal(string name) : base(name, DriverType.ANIMAL)
        {
        }
        public override double GetVelocityExtra()
        {
            return 3.0;
        }
    }
}
