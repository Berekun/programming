using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class PiereCar : Car
    {
        private Human _piloto = new Human("Pierre");
        private Animal? _copiloto = new Animal("Patan");
        public PiereCar(string name, double position) : base(name, position, ObjectType.PIERRE)
        {

        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.PIERRE;
        }

        public override void Simulate(IRace race)
        {
            if (_disableturns == 0)
            {
                if(_copiloto != null)
                    Displace(18 + _piloto.GetVelocityExtra() + _copiloto.GetVelocityExtra());
                if (Utils.GetRandom(0, 1) <= 0.5)
                {

                }
            }
        }
    }
}
