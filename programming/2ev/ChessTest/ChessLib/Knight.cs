using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Knight : FigureInternal
    {
        public Knight(int x, int y, FigureColor color) : base(x, y, color)
        {

        }
        public override FigureType GetType()
        {
            return FigureType.KNIGHT;
        }

        public static void SearchKnightPositions(IBoard board, List<Position> positionList, int x, int y, FigureColor color)
        {
            if (board.CanMove(x, y, color) == 0 || board.CanMove(x, y, color) == 1)
            {
                positionList.Add(new Position(x, y));
            }
        }

        public static List<Position> GetKnightAvaliablePosition(IBoard board, int x, int y, FigureColor color)
        {
            List<Position> positionList = new List<Position>();

            SearchKnightPositions(board, positionList, x + 1, y + 2, color);
            SearchKnightPositions(board, positionList, x - 1, y + 2, color);
            SearchKnightPositions(board, positionList, x + 2, y + 1, color);
            SearchKnightPositions(board, positionList, x - 2, y + 1, color);
            SearchKnightPositions(board, positionList, x - 1, y - 2, color);
            SearchKnightPositions(board, positionList, x + 1, y - 2, color);
            SearchKnightPositions(board, positionList, x + 2, y - 1, color);
            SearchKnightPositions(board, positionList, x - 2, y - 1, color);

            return positionList;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetKnightAvaliablePosition(board, X, Y, Color);
        }
    }
}
