using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public enum RacersType
    {
        HUMAN, ANIMAL
    }

    public abstract class Driver
    {
        private string _name;
        public Driver(string name)
        {
            _name = name;
        }
        public abstract double GetVelocityExtra(); 
    }
}
