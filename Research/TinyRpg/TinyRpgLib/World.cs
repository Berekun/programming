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
                    for (int i = 2; i < 4; i++)
                    {
                        if (i == 2)
                            portals.Add(new Portal(i, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        else
                            portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
                    }
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                            portals.Add(new Portal(i, new rect2d_f64(minX, 15, 1, 10)));
                        else if (i == 1)
                            continue;
                        else if (i == 2)
                            portals.Add(new Portal(i, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        else
                            portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
                    }
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                            portals.Add(new Portal(i, new rect2d_f64(minX, 15, 1, 10)));
                        else if (i == 1 || i == 2)
                            continue;
                        else
                            portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
                    }
                    break;
                case 3:
                    for (int i = 1; i < 4; i++)
                    {
                        if (i == 1)
                            portals.Add(new Portal(i, new rect2d_f64(15, maxX - 1, 10, 1)));
                        else if (i == 2)
                            portals.Add(new Portal(i, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        else
                            portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
                    }
                    break;
                case 4:
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
                    break;
                case 5:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                            portals.Add(new Portal(i, new rect2d_f64(minX, 15, 1, 10)));
                        else if (i == 1)
                            portals.Add(new Portal(i, new rect2d_f64(15, maxX - 1, 10, 1)));
                        else if (i == 2)
                            continue;
                        else
                            portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
                    }
                    break;
                case 6:
                    for (int i = 1; i < 3; i++)
                    {
                        if (i == 0)
                            continue;
                        else if (i == 1)
                            portals.Add(new Portal(i, new rect2d_f64(15, maxX - 1, 10, 1)));
                        else if (i == 2)
                            portals.Add(new Portal(i, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        else
                            portals.Add(new Portal(i, new rect2d_f64(15, minY, 10, 1)));
                    }   
                    break;
                case 7:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                            portals.Add(new Portal(i, new rect2d_f64(minX, 15, 1, 10)));
                        else if (i == 1)
                            portals.Add(new Portal(i, new rect2d_f64(15, maxX - 1, 10, 1)));
                        else if (i == 2)
                            portals.Add(new Portal(i, new rect2d_f64(maxX - 1, 15, 1, 10)));
                        else
                            continue;
                    }
                        
                    break;
                case 8:
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                            portals.Add(new Portal(i, new rect2d_f64(minX, 15, 1, 10)));
                        else if (i == 1)
                            portals.Add(new Portal(i, new rect2d_f64(15, maxX - 1, 10, 1)));
                        else if (i == 2)
                            continue;
                        else
                            continue;
                    }
                    break;
            }
        }
    }
}
