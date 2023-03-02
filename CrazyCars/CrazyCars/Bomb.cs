using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public class Bomb : Obstacle
    {
        public Bomb(string name, double position, int turnsToBomb) : base(name, position, ObjectType.BOMB)
        {

        }
        public override void Simulate(IRace race)
        {
            List<RaceObject> objects = WhoIsNear(race, 50);

            if (Utils.GetRandom(0, 1) < 0.2)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    Move(Utils.GetRandom(-50, 50));
                }
            }
        }
    }
}
