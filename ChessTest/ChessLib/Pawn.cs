﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Pawn : FigureInternal
    {

        public Pawn(int x, int y, FigureColor color):base(x, y ,color)
        { 
        
        }
        public override FigureType GetType()
        {
            return FigureType.PAWN;
        }

        /*public override List<Position> GetAvaliablePosition(IBoard board)
        {
            List<Position> positions = new List<Position>();
            
            if(CanMove(board,X, Y) == true)
                positions.Add(new Position(0,Y + 2));
            if (CanMove(board, X, Y) == true)
                positions.Add(new Position(0, Y + 1));
            if (board.GetFigureAt(X+1, Y+1) != null)
            
            return positions;
        }*/
    }
}
