using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public class Bomb : Obstacle
    {
        private int _gameturns;

        public Bomb(string name, double position) : base(name, position, ObjectType.BOMB)
        {
            _gameturns = 3;
        }

        public override ObjectType GetObjectType()
        {
            return ObjectType.BOMB;
        }

        public override bool IsEnabled()
        {
            if (_gameturns >= 0) return true;
            return false;
        }
        public override void Simulate(IRace race)
        {
            List<RaceObject> raceObjects = ObjectNear(race, this, 50, 50);
            if (IsEnabled() == true)
            {
                if(_gameturns == 0)
                {
                    for (int i = 0; i < raceObjects.Count; i++)
                    {
                        if (Utils.GetRandom(0, 1) <= 0.5)
                        {
                            raceObjects[i].Displace(+50);
                        }
                        else
                            raceObjects[i].Displace(-50);
                    }
                }               
                _gameturns--;
            }                          
        }
    }
}
