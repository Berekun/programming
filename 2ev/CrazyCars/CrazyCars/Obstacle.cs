using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public abstract class Obstacle : RaceObject
    {
        public Obstacle(string name, double position, ObjectType type) : base(name, position)
        {
            _type = type;
        }

        public override ObjectType GetObjectType()
        {
            return _type;
        }

        public override bool IsEnable()
        {
            return _isEnable;
        }

        public List<RaceObject> WhoIsNear(IRace race,double distance)
        {
            List<RaceObject> list = new List<RaceObject>();

            for (int i = 0; i < race.GetObjectCount(); i++)
            {
                if (Position - race.GetObjectAt(i).Position <= Math.Abs(distance))
                {
                    list.Add(race.GetObjectAt(i));
                }
            }

            return list;
        }
    }
}
