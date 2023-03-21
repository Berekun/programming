﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaMinasLib
{
    internal class BoardArray : IBoard
    {
        private Cell[] _cells;
        private int _width, _height;
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
            if(width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException("Los datos introducidos no son validos, tienen que ser mayor que 0");

            _width = width;
            _height = height;
            _cells = new Cell[CellsCount()];
        }

        public void DeleteFlagAt(int x, int y)
        {
            if (!_cells[y * _width + x].IsFlag)
                _cells[y * _width + x].SetFlag(false);
        }

        public void Init(int x, int y, int bombCount)
        {
            CreateBoard(7, 7);
            CreateCells();
            CreateBombs(bombCount, x, y);
        }

        public bool IsBombAt(int x, int y)
        {
            if (_cells[y * _width + x].IsBomb)
                return true;
            return false;
        }

        public bool IsFlagAt(int x, int y)
        {
            if (_cells[y * _width + x].IsFlag)
                return true;
            return false;
        }

        public bool IsOpen(int x, int y)
        {
            if (_cells[y * _width + x].IsOpen)
                return true;
            return false;
        }

        public void OpenCell(int x, int y)
        {
            if (!_cells[y * _width + x].IsOpen)
                _cells[y * _width + x].SetCellState(true);
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
            if (!_cells[y * _width + x].IsFlag)
                _cells[y * _width + x].SetFlag(true);

        }

        public int CellsCount()
        {
            return _width * _height;
        }

        public void CreateBombs(int bombsCount, int x, int y)
        {
            for (int i = 0; i < bombsCount; i++)
            {
                int random = Utils.GetRandomInt(0, _cells.Length - 1);
                if (!_cells[random].IsBomb && _cells[random] != _cells[y * _width + x])
                    _cells[random].SetBomb(true);
            }
        }

        public void CreateCells()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = new Cell();
            }
        }
    }
}
