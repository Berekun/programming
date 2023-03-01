using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public abstract class Car : RaceObject
    {
        private double _finetunning;
        protected List<Driver> drivers = new List<Driver>();

        public Car(string name, double position, ObjectType type) : base(name, position)
        {
            _finetunning = Utils.GetRandom(1, 3);
            _type = type;
        }

        public override ObjectType GetObjectType()
        {
            return _type;
        }

        public override void DisplacePlus(double newPosition)
        {
            double extra = 0;
            for (int i = 0; i < drivers.Count; i++)
            {
                extra += drivers[i].GetVelocityExtra();
            }

            base.DisplacePlus(newPosition + _finetunning + extra);
        }
    }
}
