using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public class Rock : Obstacle
    {
        private double weight = Utils.GetRandom(10, 30.99);
        public Rock(string name, double position) : base(name, position, ObjectType.ROCK)
        {

        }

        public override void Simulate(IRace race)
        {
            List<RaceObject> objects = WhoIsNear(race,10);

            if (Utils.GetRandom(0, 100) < 10 + weight)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].Move(-25);
                }
            }
        }
    }
}
