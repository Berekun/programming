using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExamenAutosLocos
{
    public class Race : IRace
    {
        public List<RaceObject> raceObjects= new List<RaceObject>();
        public RaceObject GetObjectAt(int index)
        {
            return raceObjects[index];
        }

        public int GetObjectCount()
        {
            throw new NotImplementedException();
        }

        public void Init(double distance)
        {
            CreateDrivers();
            AddObstacle(1000);
        }

        public void PrintDrivers()
        {
            throw new NotImplementedException();
        }

        public List<RaceObject> Simulate()
        {
            Init(1000);

            List<RaceObject> winer = new List<RaceObject>();
            while (winer == null)
            {
                foreach (var racer in raceObjects)
                {
                    racer.Simulate(this);
                    if (racer.Position >= 1000)
                    {
                        Console.WriteLine(racer.Name);
                        winer.Add(racer);
                    }   
                }
            }

            return winer;
        }

        public void CreateDrivers()
        {
            raceObjects.Add(new GlamourCar("coche1", 0));
            raceObjects.Add(new TroglodyteCar("coche2", 0));
            raceObjects.Add(new WoodCar("coche3", 0));
            raceObjects.Add(new PiereCar("coche4", 0));
        }

        public void AddObstacle(double distance)
        {
            int rockCount = 0;
            int BombsCount = 0;
            while (true)
            {
                if (rockCount == 0)
                {
                    for (int i = 0; i < Utils.GetRandom(2, 4.99); i++)
                    {
                        raceObjects.Add(new Rock("Rock", Utils.GetRandom(0, distance)));
                        rockCount++;
                    }
                }
                else if (BombsCount == 0)
                {
                    for (int i = 0; i < Utils.GetRandom(2, 4.99); i++)
                    {
                        raceObjects.Add(new Bomb("Bomb", Utils.GetRandom(0, distance)));
                        BombsCount++;
                    }
                }
                else
                {
                    for (int i = 0; i < Utils.GetRandom(2, 4.99); i++)
                    {
                        raceObjects.Add(new Charco("Charco", Utils.GetRandom(0, distance)));
                    }
                    break;
                }
            }
        }
    }
}
