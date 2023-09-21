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

        public bool isWorldClear { get; set; } = false;

        public TileWorld tileWorld;

        private Type type { get; set; } = Type.PLAINS;

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
                case Type.PLAINS:
                        GenerateObstacleOcean();
                    break;

                default:
                    break;
            }
        }

        public void GenerateObstacleOcean()
        {
            for (int i = 0; i < Tools.GetRandomInt(2,7); i++)
                obstacles.Add(new Obstacle(1,1,ObstacleType.SMALL_ROCK,GenerateRandomPosiblePosition()));
            for (int i = 0; i < Tools.GetRandomInt(1, 4); i++)
                obstacles.Add(new Obstacle(2, 2, ObstacleType.ROCK, GenerateRandomPosiblePosition()));
            //GeneratePosiblePatch(5, 15);

        }

        public Position GenerateRandomPosiblePosition()
        {
            Position pos;

            while (true)
            {
                pos = new Position(Tools.GetRandomInt(1, 39), Tools.GetRandomInt(1, 39));

                if (PosiblePosition(pos))
                    break;
            }

            return pos;
        }

        //public Position GeneratePosiblePatch(int minWeed, int maxWeed)
        //{
        //    Position pos = new Position(Tools.GetRandomInt(1, 39), Tools.GetRandomInt(1, 39));
        //    obstacles.Add(new Obstacle(1, 1, ObstacleType.WEED, pos));
        //    int x = (int)pos.X;
        //    int y = (int)pos.Y;
        //    int aux = 0;

        //    while (true)
        //    {
        //        Position position = new Position(Tools.GetRandomInt(x - 2, x + 2), Tools.GetRandomInt(y - 2, y + 2));
        //        if (IsInsideWorld(position))
        //            obstacles.Add(new Obstacle(1, 1, ObstacleType.WEED, position));

        //        foreach (Obstacle obstacle in obstacles)
        //            if (obstacle.type is ObstacleType.WEED)
        //                aux++;

        //        if() break;

        //    }
        //}

        public bool PosiblePosition(Position pos)
        {
            for (int y = 2; y > -3; y--)
            {
                for (int x = 2; x > -3; x--)
                {
                    foreach (Obstacle obstacle in obstacles)
                    {
                        if (obstacle.position.X == pos.X + x && obstacle.position.Y == pos.Y + y)
                            return false;
                    }
                }
            }

            return true;
        }

        public bool IsInsideWorld(Position pos)
        {
            if (pos.X < minX)
                return false;
            else if (pos.X > maxX - 1)
                return false;
            else if (pos.Y < minY)
                return false;
            else if (pos.Y > maxY - 1)
                return false;

            return true;
        }
    }
}
