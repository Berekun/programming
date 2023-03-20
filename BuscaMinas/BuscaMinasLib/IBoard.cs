namespace BuscaMinasLib
{
    internal interface IBoard
    {
        void CreateBoard(int width, int height);

        void Init(int x, int y, int bombCount);

        bool IsBombAt(Position pos);

        int GetBombProximity(int x, int y)
        {
            int countBomb = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if(IsBombAt(new Position(x + j, y + i)))
                        countBomb++;
                }
            }

            return countBomb;
        }

        bool IsFlagAt(Position pos);

        void SetFlagAt(int x, int y);

        void DeleteFlagAt(int x, int y);

        public bool HasWin()
        {
            if (OpenCellsCount() == WorldSize() - BombsCount())
                return true;
            return false;
        }

        bool IsOpen(Position pos);

        void OpenCell(int x, int y);

        int BombsCount();

        int OpenCellsCount();

        int WorldSize();
    }
}
