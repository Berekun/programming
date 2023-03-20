namespace BuscaMinasLib
{
    internal class Utils
    {
        public static Random random = new Random();

        public static int GetRandomInt(int min, int max)
        {
            if (min > max)
                return GetRandomInt(max, min);
            return random.Next(min,max);
        }
    }
}
