namespace BuscaMinasLib
{
    public interface IBoard
    {
        #region FUNCIONES_A_IMPLEMENTAR
        void CreateBoard(int width, int height);

        public void CreateCells();

        void Init(int x, int y, int bombCount);

        bool IsBombAt(int x, int y);

        bool IsFlagAt(int x, int y);

        void SetFlagAt(int x, int y);

        void DeleteFlagAt(int x, int y);
        bool IsOpen(int x, int y);

        void OpenCell(int x, int y);

        int BombsCount();

        int OpenCellsCount();

        int CellsCount();

        int GetHeight();

        int GetWidth();
        #endregion

        #region FUNCIONES_HECHAS
        int GetBombProximity(int x, int y)
        {
            int countBomb = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (IsBombAt(x + j, y + i))
                        countBomb++;
                }
            }

            return countBomb;
        }

        public bool HasWin()
        {
            if (OpenCellsCount() == CellsCount() - BombsCount())
                return true;
            return false;
        }
        #endregion
    }
}
