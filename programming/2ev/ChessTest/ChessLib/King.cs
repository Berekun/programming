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
            
            for (int i = 0; i < 3; i++)
            {
                if(y == Y && x + i == X)
                    continue;
                if (board.CanMove(x + i, y, color) == 0 || board.CanMove(x + i, y, color) == 1)
                {
                    if (board.AntiSuicide(this, x + i, y) == false)
                    positionList.Add(new Position(x + i, y));
                }
            }
        }

        public List<Position> GetKingAvaliablePosition(IBoard board, int x, int y, FigureColor color)
        {
            List<Position> positionList = new List<Position>();

            SearchKingPositions(board, positionList, x - 1, y + 1, color);
            SearchKingPositions(board, positionList, x - 1, y, color);
            SearchKingPositions(board, positionList, x - 1, y - 1, color);

            return positionList;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetKingAvaliablePosition(board, X, Y, Color);
        }
    }
}
