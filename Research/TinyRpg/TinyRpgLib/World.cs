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
        public int ideidentifier { get; set; }
        public double minX { get; set; }
        public double minY { get; set; }
        public double maxX { get; set; }
        public double maxY { get; set; }

        public TileWorld tileWorld;
        public List<Portal> portals { get; set; } = new List<Portal>();
        public bool IsWorldClear { get; set; } = false;
        public List<Enemigo> enemies { get; set; } = new List<Enemigo>();

        public World(int minX,int minY,int maxX, int maxY, int ideidentifier, bool isWorldClear)
        {
            tileWorld = new TileWorld(20, 20, new aabb2d_f64(minX, minY, maxX, maxY));
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
            this.ideidentifier = ideidentifier;
            this.IsWorldClear = isWorldClear;
            GeneratePortals(this.ideidentifier);
            GenerateEnemies();
            GeneratePathing();
        }

        public World()
        {

        }

        public void IsWorldClearFuncion()
        {
            if(enemies.Count <= 0)
                IsWorldClear = true;
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

        public void GenerateEnemies()
        {
            if (!IsWorldClear)
            {
                for (int i = 0; i < Tools.GetRandomInt(1, 2); i++)
                {
                    enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX + 10, (int)maxX) - 10, Tools.GetRandomInt((int)minY + 10, (int)maxY) - 10, Tools.GetRandomInt(1, 4), 20, EnemyType.DARK_WIZZARD));
                }

                //for (int i = 0; i < Tools.GetRandomInt(1, 3); i++)
                //{
                //    enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX, (int)maxX), Tools.GetRandomInt((int)minY, (int)maxY), -1, 10, EnemyType.WOLF));
                //}

                //for (int i = 0; i < Tools.GetRandomInt(1, 2); i++)
                //{
                //    enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX, (int)maxX), Tools.GetRandomInt((int)minY, (int)maxY), -1, 60, EnemyType.GOLEM));
                //}
            }
        }

        public void GeneratePathing()
        {
            foreach (Enemigo e in enemies)
            {
                int random = Tools.GetRandomInt(1, 4);
                e.pathingRoute = random;
            }
        }
    }
}
