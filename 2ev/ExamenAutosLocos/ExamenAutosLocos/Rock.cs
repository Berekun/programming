using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class Rock : Obstacle
    {
        private double _weight = Utils.GetRandom(10, 30);
        public double Weight => _weight;

        public Rock(string name, double position) : base(name, position, ObjectType.ROCK)
        {
        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.ROCK;
        }

        public override void Simulate(IRace race)
        {
            List<RaceObject> raceObjects = ObjectNear(race, this, 10, 10);

            for(int i = 0; i < raceObjects.Count; i++)
            {
                if (Utils.GetRandom(0, 100) <= 10 + _weight)
                {
                    raceObjects[i].Displace(-25);
                }
            }
        }
    }
}
