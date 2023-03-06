using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public interface IRace
    {
        public void Init(double distance);
        public List<RaceObject> Simulate();
        public void PrintDrivers();

        public int GetObjectCount();

        public RaceObject GetObjectAt(int index);
    }
}
