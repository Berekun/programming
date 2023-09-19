using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UDK;

namespace TinyRpgLib
{
    enum Type
    {
        OCEAN, BEACH, PLAINS, SWAMP, DESERT, SNOWY_MOUNTAIN, VOLCANO, OCEAN_CAVE, CAVE
    }

    public class World
    {
        //public int ideidentifier { get; set; }
        public double minX { get; set; }
        public double minY { get; set; }
        public double maxX { get; set; }
        public double maxY { get; set; }

        public TileWorld tileWorld;

        private Type type { get; set; }

        //public List<Portal> portals { get; set; } = new List<Portal>();
        public List<Enemigo> enemies { get; set; } = new List<Enemigo>();
        public List<Obstacle> obstacles { get; set; } = new List<Obstacle>();

        public World(int minX,int minY,int maxX, int maxY)
        {
            tileWorld = new TileWorld(20, 20, new aabb2d_f64(minX, minY, maxX, maxY));
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
            //GeneratePortals(this.ideidentifier);
            GenerateEnemies();
            GenerateObstacle();
        }

        public World()
        {

        }

        //public void GeneratePortals(int identifier) //Optimizable
        //{
        //    switch (identifier)
        //    {
        //        case 0:
        //                portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
        //                portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
        //            break;
        //        case 1:
        //                portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
        //                portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
        //                portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
        //            break;
        //        case 2:
        //                portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
        //                portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
        //            break;
        //        case 3:
        //                portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
        //                portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
        //                portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
        //            break;
        //        case 4:
        //                portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
        //                portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
        //                portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
        //                portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
        //            break;
        //        case 5:
        //                portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
        //                portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
        //                portals.Add(new Portal(3, new rect2d_f64(15, minY, 10, 1)));
        //            break;
        //        case 6:
        //                portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
        //                portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
        //            break;
        //        case 7:
        //                portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
        //                portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
        //                portals.Add(new Portal(2, new rect2d_f64(maxX - 1, 15, 1, 10)));
        //            break;
        //        case 8:
        //                portals.Add(new Portal(0, new rect2d_f64(minX, 15, 1, 10)));
        //                portals.Add(new Portal(1, new rect2d_f64(15, maxX - 1, 10, 1)));
        //            break;
        //    }
        //}

        public void GenerateEnemies()
        {
                //for (int i = 0; i < Tools.GetRandomInt(1, 2); i++)
                //{
                //    enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX + 10, (int)maxX) - 10, Tools.GetRandomInt((int)minY + 10, (int)maxY) - 10, 20, EnemyType.DARK_WIZZARD));
                //}

                //for (int i = 0; i < Tools.GetRandomInt(1, 3); i++)
                //{
                //    enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX, (int)maxX), Tools.GetRandomInt((int)minY, (int)maxY), 10, EnemyType.WOLF));
                //}

                //for (int i = 0; i < Tools.GetRandomInt(0, 2); i++)
                //{
                //    enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX, (int)maxX), Tools.GetRandomInt((int)minY, (int)maxY), 60, EnemyType.GOLEM));
                //}
        }

        public void GenerateObstacle()
        {
            switch (this.type)
            {
                case Type.OCEAN:
                        GenerateObstacleOcean();
                    break;

                default:
                    break;
            }
        }

        public void GenerateObstacleOcean()
        {
            for (int i = 0; i < Tools.GetRandomInt(1,5); i++)
            {
                obstacles.Add(new Obstacle(1,1,ObstacleType.SMALL_ROCK,GenerateRandomPosiblePosition(ObstacleType.SMALL_ROCK)));
            }
        }

        public Position GenerateRandomPosiblePosition(ObstacleType type)
        {
            int aux = ReviewQuadrant(1, obstacles, type);
            Position pos = new Position();

            if (aux == 1)
            {
                pos.X = Tools.GetRandomInt(1, 21);
                pos.Y = Tools.GetRandomInt(1, 21);
            }
            else if (aux == 2)
            {
                pos.X = Tools.GetRandomInt(20, 39);
                pos.Y = Tools.GetRandomInt(1, 21);
            }
            else if (aux == 3)
            {
                pos.X = Tools.GetRandomInt(1, 21);
                pos.Y = Tools.GetRandomInt(20, 39);
            }
            else
            {
                pos.X = Tools.GetRandomInt(20, 39);
                pos.Y = Tools.GetRandomInt(20, 39);
            }

            return pos;
        }

        public int ReviewQuadrant(int numberoOfObstacles, List<Obstacle> obstacles, ObstacleType type)
        {
            int aux = 0;    

            foreach (Obstacle obstacle in obstacles)
            {
                for (int x = 0; x <= 20; x++)
                {
                    for (int y = 0; y <= 20; y++)
                    {
                        if (obstacle.position.X == x && obstacle.position.Y == y)
                            aux++;
                    }
                }
            }

            if (aux == 0)
                return 1;
            else 
                aux = 0;

            foreach (Obstacle obstacle in obstacles)
            {
                for (int x = 21; x <= 40; x++)
                {
                    for (int y = 0; y <= 20; y++)
                    {
                        if (obstacle.position.X == x && obstacle.position.Y == y)
                            aux++;
                    }
                }
            }

            if (aux == 0)
                return 2;
            else
                aux = 0;

            foreach (Obstacle obstacle in obstacles)
            {
                for (int x = 0; x <= 20; x++)
                {
                    for (int y = 21; y <= 40; y++)
                    {
                        if (obstacle.position.X == x && obstacle.position.Y == y)
                            aux++;
                    }
                }
            }

            if (aux == 0)
                return 3;
            else
                aux = 0;

            foreach (Obstacle obstacle in obstacles)
            {
                for (int x = 21; x <= 40; x++)
                {
                    for (int y = 21; y <= 40; y++)
                    {
                        if (obstacle.position.X == x && obstacle.position.Y == y)
                            aux++;
                    }
                }
            }

            if (aux == 0)
                return 4;
            

            return -1;
        }
    }
}
