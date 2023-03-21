namespace BuscaMinasLib
{
    internal interface IBoard
    {
        void CreateBoard(int width, int height);

        public void CreateBombs(int bombsCount, int x, int y);

        public void CreateCells();

        void Init(int x, int y, int bombCount);

        bool IsBombAt(int x, int y);

        int GetBombProximity(int x, int y)
        {
            int countBomb = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if(IsBombAt(x + j, y + i))
                        countBomb++;
                }
            }

            return countBomb;
        }

        bool IsFlagAt(int x, int y);

        void SetFlagAt(int x, int y);

        void DeleteFlagAt(int x, int y);

        public bool HasWin()
        {
            if (OpenCellsCount() == CellsCount() - BombsCount())
                return true;
            return false;
        }

        bool IsOpen(int x, int y);

        void OpenCell(int x, int y);

        int BombsCount();

        int OpenCellsCount();

        int CellsCount();
    }
}
