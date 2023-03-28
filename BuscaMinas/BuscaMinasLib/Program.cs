namespace BuscaMinasLib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBoard board = new BoardArrayBi();
            Test1(board);
        }

        public static void Test1(IBoard board)
        {
            board.CreateBoard(2, 2);
            board.Init(0, 0, 1);
            board.GetBombProximity(0, 0);
            board.IsBombAt(1, 1);
            board.SetFlagAt(1, 1);
            board.DeleteFlagAt(1, 1);
            board.OpenCell(0, 0);
            board.OpenCell(1, 0);
            board.OpenCell(1, 1);
            board.IsOpen(1, 1);
            board.BombsCount();
            board.HasWin();
        }
    }
}