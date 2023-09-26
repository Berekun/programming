﻿using System;
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
            for (int i = 0; i < Tools.GetRandomInt(1, 2); i++)
            {
                enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX + 10, (int)maxX) - 10, Tools.GetRandomInt((int)minY + 10, (int)maxY) - 10, 20, EnemyType.DARK_WIZZARD, 2, 2));
            }

            for (int i = 0; i < Tools.GetRandomInt(1, 3); i++)
            {
                enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX, (int)maxX), Tools.GetRandomInt((int)minY, (int)maxY), 10, EnemyType.WOLF, 1, 1));
            }

            for (int i = 0; i < Tools.GetRandomInt(0, 2); i++)
            {
                enemies.Add(new Enemigo(Tools.GetRandomInt((int)minX, (int)maxX), Tools.GetRandomInt((int)minY, (int)maxY), 60, EnemyType.GOLEM, 1, 1));
            }
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
            for (int i = 0; i < Tools.GetRandomInt(4,7); i++)
                obstacles.Add(new Obstacle(1,1,ObstacleType.SMALL_ROCK,GenerateRandomPosiblePosition(1,1)));
            for (int i = 0; i < Tools.GetRandomInt(1, 4); i++)
                obstacles.Add(new Obstacle(2, 2, ObstacleType.ROCK, GenerateRandomPosiblePosition(2,2)));
            for(int i = 0; i < 3; i++)
                GeneratePosiblePatch(10, 20);

        }

        public Position GenerateRandomPosiblePosition(double maxX, double maxY)
        {
            Position pos;

            while (true)
            {
                pos = new Position(Tools.GetRandomInt(1, 39), Tools.GetRandomInt(1, 39), maxX, maxY);

                if (PosiblePosition(pos))
                    break;
            }

            return pos;
        }

        public void GeneratePosiblePatch(int minWeed, int maxWeed)
        {
            Position pos = new Position(Tools.GetRandomInt(1, 39), Tools.GetRandomInt(1, 39), 1, 1);
            obstacles.Add(new Obstacle(1, 1, ObstacleType.WEED, pos));
            int weedCount = Tools.GetRandomInt(minWeed, maxWeed);
            int aux = 0;

            while (true)
            {
                Position position = new Position(Tools.GetRandomInt((int)pos.X - 2, (int)pos.X + 2), Tools.GetRandomInt((int)pos.Y - 2, (int)pos.Y + 2), 1, 1);
                if (IsInsideWorld(position) && !IsObstacleAt(position))
                {
                    obstacles.Add(new Obstacle(1, 1, ObstacleType.WEED, position));
                    aux++;
                }
                    
                if (aux == weedCount) 
                    break;
            }
        }

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
            if (pos.X < minX + 1)
                return false;
            else if (pos.X > maxX - 2)
                return false;
            else if (pos.Y < minY + 1)
                return false;
            else if (pos.Y > maxY - 2)
                return false;

            return true;
        }

        public bool IsObstacleAt(Position pos)
        {
            foreach (Obstacle obstacle in obstacles)
            {   
                if(obstacle.position == pos)
                    return true;
            }

            return false;
        }
    }
}
