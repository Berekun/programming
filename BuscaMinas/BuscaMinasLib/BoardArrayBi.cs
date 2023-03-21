using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace BuscaMinasLib
{
    internal class BoardArrayBi : IBoard
    {
        private Cell[,] _cells;
        private int _width, _height;
        public int BombsCount()
        {
            int count = 0;
            foreach (var cell in _cells)
            {
                if(cell.IsBomb)
                    count++;
            }
            return count;
        }

        public int CellsCount()
        {
            return _width * _height;
        }

        public void CreateBoard(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException("Los datos introducidos no son validos, tienen que ser mayor que 0");

            _width = width;
            _height = height;
            _cells = new Cell[width, height];
        }

        public void CreateBombs(int bombsCount, int x, int y)
        {
            for (int i = 0; i < bombsCount; i++)
            {
                int randomx = Utils.GetRandomInt(0, _width);
                int randomy = Utils.GetRandomInt(0, _height);
                if (_cells[randomx, randomy] != _cells[x, y] && !IsBombAt(randomx, randomy))
                    _cells[x, y].SetBomb(true);
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
                    _cells[k,j] = new Cell();
                }
            }
        }

        public void DeleteFlagAt(int x, int y)
        {
            if (_cells[x, y].IsFlag)
                _cells[x, y].SetFlag(false);        }

        public void Init(int x, int y, int bombCount)
        {
            CreateBoard(7, 7);
            CreateCells();
            CreateBombs(bombCount, x, y);
        }

        public bool IsBombAt(int x, int y)
        {
            if (_cells[x, y].IsBomb)
                return true;
            return false;
        }

        public bool IsFlagAt(int x, int y)
        {
            if (_cells[x, y].IsFlag)
                return true;
            return false;
        }

        public bool IsOpen(int x, int y)
        {
            if (_cells[x, y].IsOpen)
                return true;
            return false;
        }

        public void OpenCell(int x, int y)
        {
            if (!_cells[x, y].IsOpen)
                _cells[x, y].SetCellState(true);
        }

        public int OpenCellsCount()
        {
            int count = 0;
            foreach (var cell in _cells)
            {
                if (cell.IsFlag)
                    count++;
            }
            return count;
        }

        public void SetFlagAt(int x, int y)
        {
            if (!_cells[x,y].IsFlag)
                _cells[x,y].SetFlag(true);
        }
    }
}
