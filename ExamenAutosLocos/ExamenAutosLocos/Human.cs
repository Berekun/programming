using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class Human : Driver
    {
        public Human(string name) : base(name, DriverType.HUMAN)
        {

        }

        public override double GetVelocityExtra()
        {
            return 0.0; 
        }
    }
}
