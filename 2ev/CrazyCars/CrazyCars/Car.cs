using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public abstract class Car : RaceObject
    {
        protected List<Driver> drivers = new List<Driver>();
        public Car(string name,double position, ObjectType type) : base(name, position)
        {
            finetunning = Utils.GetRandom(1, 3.99);
            type = _type;
        }

        protected double finetunning;
        public override ObjectType GetObjectType()
        {
            return _type;
        }

        public override bool IsEnable()
        {
            return _isEnable;
        }

        public override void Move(double position)
        {
            base.Move(position);
        }

        public virtual void MoveComp(double position)
        {
            Move(position + finetunning);
        }

        public override void VisitDriver(Visitor visit)
        {
            foreach (Driver driver in drivers)
            {
                visit(driver);
            }
        }
    }
}
