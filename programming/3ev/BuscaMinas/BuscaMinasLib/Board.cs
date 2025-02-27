namespace BuscaMinasLib
{
    public class Board : IBoard
    {
        #region ATRIBUTOS
        private List<Position> _bombs = new List<Position>();
        private List<Position> _flags = new List<Position>();
        private int _width, _height;
        private List<Position> _openCells = new List<Position>();
        #endregion

        #region FUNCIONES_IBOARD
        public void CreateBoard(int width, int height)
        {
            if(width < 2 || height < 2)
                throw new ArgumentOutOfRangeException("Los valores para el mundo son menores que 2");

            _width = width;
            _height = height;
        }
        public void DeleteFlagAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            for(int i = 0; i < _flags.Count; i++)
            {
                if (IsFlagAt(x, y) && (_flags[i].X == x && _flags[i].Y == y))
                {
                    _flags.RemoveAt(i);
                }
            }
           
        }

        public void Init(int x, int y, int bombCount)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            CreateCells();
            Utils.CreateBombs(bombCount,_width,_height, (xx, yy) =>
            {
                if (!IsBombAt(xx, yy) && (x != xx || y != yy))
                {
                    _bombs.Add(new Position(xx, yy));
                    return true;
                }
                return false;                
            });
        }

        public void OpenCell(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            Position aux = new Position(x, y);
            if (!IsFlagAt(x, y) && !IsOpen(x, y))
            {
                _openCells.Add(aux);
            }
        }

        public void SetFlagAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return;

            Position aux = new Position(x, y);
            if (!IsFlagAt(x, y) && !IsOpen(x, y))
            {
                _flags.Add(aux);
            }
        }

        // Javi: O property o función, ...
        public int BombsCount()
        {
            return _bombs.Count;
        }

        public int OpenCellsCount()
        {
            return _openCells.Count;
        }

        public int CellsCount()
        {
            return _width * _height;
        }

        public bool IsBombAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return false;

            return Utils.IsElementAt(_bombs, x, y);
        }

        public bool IsFlagAt(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return false;

            return Utils.IsElementAt(_flags, x, y);
        }

        public bool IsOpen(int x, int y)
        {
            if (!Utils.IsValueOnRange(x, y, this))
                return false;

            return Utils.IsElementAt(_openCells, x, y);
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
        public void CreateCells()
        {
            for (int j = 0; j < _height; j++)
            {
                for (int k = 0; k < _width; k++)
                {
                    new Position(k, j);
                }
            }
        }
        #endregion
    }
}
