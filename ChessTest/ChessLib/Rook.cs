using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Rook : FigureInternal
    {
        public Rook(int x, int y, FigureColor color) : base(x, y, color)
        {

        }
        public override FigureType GetType()
        {
            return FigureType.ROOK;
        }

        public static List<Position> GetRookAvaliablePosition(IBoard board, int startx,int starty, FigureColor color)
        {
            List<Position> positionList = new List<Position>();

            for(int x = 0; x < 8; x++)
            {
                if (board.CanMove(x, starty, color) == true)
                    positionList.Add(new Position(x, starty));
            }
            
            return positionList;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetRookAvaliablePosition();
        }
    }
}
