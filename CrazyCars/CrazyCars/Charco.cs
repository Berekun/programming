using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public class Charco : Obstacle 
    {
        public Charco(string name, double position) : base(name, position, ObjectType.CHARCO)
        {

        }

        public override void Simulate(IRace race)
        {
            List<RaceObject> objects = WhoIsNear(race,20);

            if (Utils.GetRandom(0, 1) < 0.2)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].Disable((int)Utils.GetRandom(0, 3.99));
                }
            }
        }
    }
}
