namespace BuscaMinasLib
{
    public class BoardArray : IBoard
    {
        #region ATRIBUTOS
            private Cell[] _cells;
            private int _width, _height;
        #endregion

        #region FUNCIONES_IBOARD
        public int BombsCount()
        {
            int count = 0;
            foreach (var cell in _cells)
            {
                if (cell.IsBomb)
                    count++;
            }

            return count;
        }

        public void CreateBoard(int width, int height)
        {
            if(width < 1 || height < 1)
                throw new ArgumentOutOfRangeException("Los datos introducidos no son validos, tienen que ser mayor que 1");

            _width = width;
            _height = height;
            _cells = new Cell[CellsCount()];
        }

        public void DeleteFlagAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            if (_cells[PositionToIndexArray(x, y)].IsFlag)
                _cells[PositionToIndexArray(x, y)].SetFlag(false);
        }

        public void Init(int x, int y, int bombCount)
        {
            if (!Utils.IsValueOnRange(x, y, this) || bombCount > CellsCount() / 2)
                return;

            CreateCells();
            Utils.CreateBombs(bombCount,_width,_height, (xx,yy) =>
            {
                if (!IsBombAt(xx, yy) && (x != xx || y != yy))
                {
                    _cells[PositionToIndexArray(xx, yy)].SetBomb(true);
                    return true;
                }
                return false;
            });
        }

        public bool IsBombAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return false;

            if (_cells[PositionToIndexArray(x, y)].IsBomb)
                return true;
            return false;
        }

        public bool IsFlagAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return false;

            if (_cells[PositionToIndexArray(x, y)].IsFlag)
                return true;
            return false;
        }

        public bool IsOpen(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return false;

            if (_cells[PositionToIndexArray(x, y)].IsOpen)
                return true;
            return false;
        }

        public void OpenCell(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            if (!_cells[PositionToIndexArray(x, y)].IsOpen)
                _cells[PositionToIndexArray(x, y)].SetCellState(true);
        }

        public int OpenCellsCount()
        {
            int count = 0;
            foreach (var cell in _cells)
            {
                if (cell.IsOpen)
                    count++;
            }

            return count;
        }
        public void SetFlagAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            if (!_cells[PositionToIndexArray(x, y)].IsFlag)
                _cells[PositionToIndexArray(x, y)].SetFlag(true);
        }

        public int CellsCount()
        {
            return _width * _height;
        }

        public int GetHeight()
        {
            return _height;
        }

        public int GetWidth()
        {
            return _width;
        }
        #endregion

        #region FUNCIONES_NO_IBOARD
        public int PositionToIndexArray(int x, int y)
        {
            return y * _width + x;
        }

        public void CreateCells()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = new Cell();
            }
        }
        #endregion
    }
}
