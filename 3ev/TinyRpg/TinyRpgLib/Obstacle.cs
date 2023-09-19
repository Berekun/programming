using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public enum ObstacleType
    {
        SMALL_ROCK, ROCK, WEED
    }

    public class Obstacle
    {
        public int width { get; set; }

        public int height { get; set; }

        public ObstacleType type { get; set; }

        public Position position { get; set; }

        public Obstacle(int width, int height, ObstacleType type, Position pos)
        {
            this.width = width;
            this.height = height;
            this.type = type;
            this.position = pos;
        }
    }
}
