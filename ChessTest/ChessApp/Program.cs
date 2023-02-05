using DAM;

namespace ChessApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DAM.Game.Launch(new MyChessGame());
        }
    }
}