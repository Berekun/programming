using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrera
{
    public enum RunnerType
    {
        Marathon,Speedster,Thief
    }

    internal class Racer
    {
        public string name;
        public double position;
        public RunnerType type;

        public void Advanceposition()
        {
            switch(this.type)
            {
                case RunnerType.Marathon:
                    this.position += Utils.Getrandom(5,15);
                break;

                case RunnerType.Speedster:
                    this.position += Utils.Getrandom(0,25);
                break;

                default:
                    this.position += Utils.Getrandom(0,20);
                break;
            }
        }
    }
}
