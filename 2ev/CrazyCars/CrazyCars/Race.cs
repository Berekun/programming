using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public delegate void Visitor(Driver driver);
    public class Race : IRace
    {
        private List<RaceObject> _objects = new List<RaceObject>();
        public RaceObject GetObjectAt(int index)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if(index == i)
                    return _objects[i];
            }

            return null;
        }

        public int GetObjectCount()
        {
            return _objects.Count;
        }

        public void DeleteObject(RaceObject obj)
        {
            if(obj.IsEnable() == false) 
            _objects.Remove(obj);
        }

        public void Iniciar(double distance)
        {
            AddCar();
            AddObstacle(distance);

        }

        public void Visit(Visitor visit)
        {
            foreach (RaceObject obj in _objects)
            {
                if (obj is Car)
                {
                    obj.VisitDriver(visit);
                }
            }

        }

        public List<RaceObject> Simulate()
        {
            int distance = 1000;
            List<RaceObject> winner = new List<RaceObject>();  

            Iniciar(distance);

            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Simulate(this);
                if (_objects[i].Position > distance)
                    winner.Add(_objects[i]);
            }

            if(winner != null)
                return winner;
            return null;
        }

        public void AddCar()
        {
            _objects.Add(new GlamourCar("Glamour", 0.0));
            _objects.Add(new WoodCar("Wood", 0.0));
            _objects.Add(new TroglodyteCar("Troglodyte", 0.0));
            _objects.Add(new PiereCar("Piere", 0.0));
        }

        public void AddObstacle(double distance)
        {
            int rockCount = 0;
            int BombsCount = 0;

            if(rockCount == 0)
            {
                    for (int i = 0; i < Utils.GetRandom(2, 4.99); i++)
                    {
                        _objects.Add(new Rock("Rock", Utils.GetRandom(0, distance)));
                    }
            }
            else if (BombsCount == 0)
            {
                for (int i = 0; i < Utils.GetRandom(2, 4.99); i++)
                {
                    _objects.Add(new Bomb("Bomb", Utils.GetRandom(0, distance), (int)Utils.GetRandom(2,7)));
                }
            }
            else
            {
                for (int i = 0; i < Utils.GetRandom(2, 4.99); i++)
                {
                    _objects.Add(new Charco("Charco", Utils.GetRandom(0, distance)));
                }
            }
        }
    }
}
