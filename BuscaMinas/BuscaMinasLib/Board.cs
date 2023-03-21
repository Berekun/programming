namespace BuscaMinasLib
{
    internal class Board : IBoard
    {
        private List<Position> _bombs = new List<Position>();
        private List<Position> _flags = new List<Position>();
        private int _width, _height;
        private List<Position> _openCells = new List<Position>();

        public void CreateBoard(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void CreateBombs(int bombsCount, int x, int y)
        {
            Position aux;
            Position pos = new Position(x, y);

            for (int i = 0; i < bombsCount; i++)
            {
                int randomx = Utils.GetRandomInt(0, _width);
                int randomy = Utils.GetRandomInt(0, _height);
                aux = new Position(randomx, randomy);
                if (aux != pos && !IsBombAt(randomx, randomy))
                    _bombs.Add(aux);
                else
                    i--;
            }
        }

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

        public void DeleteFlagAt(int x, int y)
        {
            Position aux = new Position(x, y);
            if (!IsFlagAt(x,y))
            {
                _flags.Remove(aux);
            }
        }

        public void Init(int x, int y, int bombCount)
        {
            CreateBoard(7, 7);
            CreateCells();
            CreateBombs(bombCount, x, y);
        }

        public bool IsBombAt(int x, int y)
        {
            Position position = new Position(x, y);
            foreach (Position pos in _bombs)
            {
                if (pos == position)
                    return true;
            }

            return false;
        }

        public bool IsFlagAt(int x, int y)
        {
            Position position = new Position(x, y);
            foreach (Position pos in _flags)
            {
                if (pos == position)
                    return true;
            }

            return false;
        }

        public bool IsOpen(int x, int y)
        {
            Position position = new Position(x, y);
            foreach (Position pos in _openCells)
            {
                if (pos == position)
                    return true;
            }

            return false;
        }

        public void OpenCell(int x, int y)
        {
            Position aux = new Position(x, y);

            if (!IsFlagAt(x,y) && !IsOpen(x,y))
            {
                _openCells.Add(aux);
            }
        }

        public void SetFlagAt(int x, int y)
        {
            Position aux = new Position(x, y);
            if (!IsOpen(x,y) && !IsFlagAt(x,y))
            {
                _flags.Add(aux);
            }
        }

        public int BombsCount()
        {
            return _bombs.Count;
        }

        public int OpenCellsCount()
        {
            return _bombs.Count;
        }

        public int CellsCount()
        {
            return _width * _height;
        }
    }
}
