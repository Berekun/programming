using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class King : FigureInternal
    {
        public King(int x, int y, FigureColor color) : base(x, y, color)
        {

        }
        public override FigureType GetType()
        {
            return FigureType.KING;
        }

        public void SearchKingPositions(IBoard board, List<Position> positionList, int x, int y, FigureColor color)
        {
            Position? p = board.AntiSuicide(this);
            for (int i = 0; i < 3; i++)
            {
                if (board.CanMove(x + i, y, color) == 0 || board.CanMove(x + i, y, color) == 1)
                {
                    positionList.Add(new Position(x + i, y));
                }
                if(p != null)
                {
                    positionList.Remove(p);
                }
            }
        }

        public List<Position> GetKingAvaliablePosition(IBoard iboard, int x, int y, FigureColor color)
        {
            List<Position> positionList = new List<Position>();

            SearchKingPositions(iboard, positionList, x - 1, y + 1, color);
            SearchKingPositions(iboard, positionList, x - 1, y, color);
            SearchKingPositions(iboard, positionList, x - 1, y - 1, color);

            return positionList;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetKingAvaliablePosition(board, X, Y, Color);
        }
    }
}
