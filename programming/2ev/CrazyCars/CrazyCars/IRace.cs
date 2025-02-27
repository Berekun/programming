using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyCars
{
    public interface IRace
    {
        public void Iniciar(double distance);
        public List<RaceObject> Simulate();
        public void Visit(Visitor visit);
        public int GetObjectCount();
        public RaceObject GetObjectAt(int index);   
    }
}
