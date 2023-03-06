using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    internal class Charco : Obstacle
    {
        public Charco(string name, double position) : base(name, position, ObjectType.CHARCO)
        {
        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.CHARCO;
        }

        public override void Simulate(IRace race)
        {
            List<RaceObject> raceObjects = ObjectNear(race, this, 20, 20);

            for (int i = 0; i < raceObjects.Count; i++)
            {
                if (Utils.GetRandom(0, 100) <= 20)
                {
                    raceObjects[i].Disable((int)Utils.GetRandom(0,3));
                }
            }
        }
    }
}
