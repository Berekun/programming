using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrera
{
    internal class Racer
    {
        public string name;
        public double position;

        public void Advanceposition()
        {
            this.position += 10;
        }
    }
}
