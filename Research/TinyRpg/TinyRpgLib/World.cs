using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDK;

namespace TinyRpgLib
{
    public class World
    {
        public double ideidentifier { get; set; } = 5;
        public double minX { get; set; }
        public double minY { get; set; }
        public double maxX { get; set; }
        public double maxY { get; set; }

        public List<Portal> portals = new List<Portal>();

        public World(int minX,int minY,int maxX, int maxY, int ideidentifier)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
            this.ideidentifier = ideidentifier;
        }

        public World()
        {

        }

        public void GeneratePortals(int identifier)
        {
            //switch (identifier)
            //{
            //    case 0:
            //    case 2:
            //    case 6:
            //    case 8:
            //        for (int i = 2; i < 4; i++)
            //            portals.Add(new Portal(i, new rect2d_f64(0, 0, 10, 1)));
            //        break;
            //    case 1:
            //    case 3:
            //    case 5:
            //    case 7:
            //        for (int i = 0; i < 2; i++)
            //            portals.Add(new Portal(3, new rect2d_f64(0, 0, 10, 1)));
            //        break;
            //}
        }
    }
}
