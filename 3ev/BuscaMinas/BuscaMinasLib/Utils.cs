using System.Runtime.Intrinsics.X86;

namespace BuscaMinasLib
{
    public delegate bool SetBombDelegate(int x, int y);
    public class Utils
    {
        #region ATRIBUTOS
        public static Random random = new Random();
        #endregion

        #region FUNCIONES
        public static int GetRandomInt(int min, int max)
        {
            if (min > max)
                return GetRandomInt(max, min);
            return random.Next(min, max);
        }

        public static double GetRadomDouble(int min, int max)
        {
            if (min > max)
                return GetRadomDouble(max, min);
            return random.NextDouble() * (max - min) + min;
        }


        public static void CreateBombs(int bombsCount,int width,int height, SetBombDelegate bombdelegate)
        {
            if (bombsCount > width * height)
                return;

            for (int i = 0; i < bombsCount; i++)
            {
                int randomx = Utils.GetRandomInt(0, width);
                int randomy = Utils.GetRandomInt(0, height);
                if (bombdelegate(randomx, randomy))
                    continue;
                else
                    i--;
            }
        }   

        public static bool IsElementAt(List<Position> list, int x, int y)
        {
            Position position = new Position(x, y);
            foreach (Position pos in list)
            {
                if (pos.X == position.X && pos.Y == position.Y)
                    return true;
            }

            return false;
        }

        public static bool IsValueOnRange(int x, int y, IBoard board)
        {
            if (x < 0 || y < 0 || x >= board.GetWidth() || y >= board.GetHeight())
                return false;
            return true;
        }

        #endregion
    }
}
