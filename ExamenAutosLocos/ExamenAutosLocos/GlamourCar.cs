using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class GlamourCar : Car
    {
        private Human _piloto = new Human("Laura");  
        public GlamourCar(string name, double position) : base(name, position, ObjectType.GLAMOUR)
        {
        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.GLAMOUR;
        }

        public override void Simulate(IRace race)
        {
            if (_disableturns == 0)
            {
                DisplacePlus(20);
            }
            Disable(-1);
        }
    }
}
