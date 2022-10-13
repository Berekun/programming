using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrera
{
    internal class Simulation
    {  
        
        public static List<Racer> GetRacers()
        {
            List<Racer> racers = new List<Racer>();

            Racer a = new Racer();
            a.name = "Nacho";
            a.position = 0.0;

            Racer b = new Racer();
            b.name = "Pablo";
            b.position = 0.0;

            Racer c = new Racer();
            c.name = "Daniel";
            c.position = 0.0;

            Racer d = new Racer();
            d.name = "Nico";
            d.position = 0.0;

            racers.Add(a);
            racers.Add(b);
            racers.Add(c);
            racers.Add(d);

            return racers;
        }
        public static void MoveRacers(List<Racer> list)
        {

            for(int i = 0; i < list.Count; i++)
            {
                list[i].Advanceposition();
            }
        }

        public static Racer GetWinner(List<Racer> list)
        {
            for(int i = 0; i < list.Count;i++)
            {
                if (list[i].position >= 1000)
                    return list[i];

            }

            return null;
        }

      
    }
}
