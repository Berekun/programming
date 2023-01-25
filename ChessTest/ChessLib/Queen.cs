using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Queen : FigureInternal
    {
        public Queen(int x, int y, FigureColor color) : base(x, y, color)
        {

        }
        public override FigureType GetType()
        {
            return FigureType.QUEEN;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetQueenAvaliablePosition(board, X, Y, Color);
        }

        public static List<Position> GetQueenAvaliablePosition(IBoard board, int x, int y, FigureColor color)
        {
            List<Position> list = new List<Position>();
            list.AddRange(Rook.GetRookAvaliablePosition(board,x,y,color));
            list.AddRange(Bishop.GetBishopAvaliablePosition(board, x, y, color));
            return list;

        }
    }
}
