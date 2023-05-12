using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UDK;

namespace TinyRpgLib
{
    public class World
    {
        public int ideidentifier { get; set; } = 5;
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
            GeneratePortals(this.ideidentifier);
        }

        public World()
        {
            GeneratePortals(this.ideidentifier);
        }

        public void GeneratePortals(int identifier) //Optimizable
        {
            switch (identifier)
            {
                case 0:
                        portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
                    break;
                case 1:
                        portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
                        portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
                    break;
                case 2:
                        portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
                        portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
                    break;
                case 3:
                        portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
                        portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
                    break;
                case 4:
                        portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
                        portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
                        portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
                    break;
                case 5:
                        portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
                        portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
                        portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
                    break;
                case 6:
                        portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
                        portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
                    break;
                case 7:
                        portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
                        portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
                        portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
                    break;
                case 8:
                        portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
                        portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
                    break;
            }
        }


        public void GeneratePortals1()
        {
            rect2d_f64[,] bi = new rect2d_f64[,] { };

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                    portals.Add(new Portal(i, new rect2d_f64(minX, 15, 1, 10)));
                else if (i == 1)
                    portals.Add(new Portal(i, new rect2d_f64(15, maxX - 1, 10, 1)));
                else if (i == 2)
                    portals.Add(new Portal(i, new rect2d_f64(maxX - 1, 15, 1, 10)));
                else
                    portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
            }
        }
    }
}
