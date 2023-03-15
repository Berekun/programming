﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaMinasLib
{
    internal class Board : IBoard
    {
        private List<Position> _bombs = new List<Position>();
        private List<Position> _flags = new List<Position>();
        private int _width, _height;
        private List<Position> _openCells = new List<Position>();
        private List<Position> _cells = new List<Position>();

        public void CreateBoard(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void CreateBombs(int bombsCount, Position pos)
        {
            Position aux;

            for (int i = 0; i < bombsCount; i++)
            {
                int random = Utils.GetRandomInt(0, _cells.Count - 1);
                aux = _cells[random];
                if (aux != pos)
                {
                    _bombs.Add(aux);
                    _cells.RemoveAt(random);
                }
                
            }
        }

        public void CreateCells()
        {
            int numberCells = _width * _height;

            for (int i = 0; i < numberCells; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    for (int k = 0; k < _width; k++)
                    {
                        _cells.Add(new Position(k, j));
                    }
                }
            }
        }

        public void DeleteFlagAt(int x, int y)
        {
            Position aux = new Position(x, y);
            if (!IsFlagAt(aux))
            {
                _flags.Remove(aux);
            }
        }

        public int GetBombProximity(int x, int y)
        {
            
        }

        public void Init(int x, int y, int bombCount)
        {
            Position firstPos = new Position(x, y);
            CreateCells();
            CreateBombs(bombCount, firstPos);
        }

        public bool IsBombAt(Position position)
        {
            foreach (Position pos in _bombs)
            {
                if (pos == position)
                    return true;
            }

            return false;
        }

        public bool IsFlagAt(Position position)
        {
            foreach (Position pos in _flags)
            {
                if (pos == position)
                    return true;
            }

            return false;
        }

        public bool IsOpen(Position position)
        {
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

            if (!IsFlagAt(aux) && !IsOpen(aux))
            {
                _openCells.Add(aux);
                _cells.Remove(aux);
            }
        }

        public void SetFlagAt(int x, int y)
        {
            Position aux = new Position(x, y);
            if (!IsOpen(aux))
            {
                _flags.Add(aux);
                _cells.Remove(aux);
            }
        }
    }
}
