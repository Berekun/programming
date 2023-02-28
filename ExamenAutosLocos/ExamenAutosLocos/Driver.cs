using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public enum DriverType
    {
        ANIMAL, HUMAN
    }

    public abstract class Driver
    {
        private string _name;       
        public Driver(string name, DriverType type)
        {
            _name = name;
        }
        public abstract double GetVelocityExtra();
    }
}
