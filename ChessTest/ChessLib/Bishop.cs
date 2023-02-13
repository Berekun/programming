using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Bishop : FigureInternal
    {
        public Bishop(int x, int y, FigureColor color) : base(x, y, color)
        {

        }
        public override FigureType GetType()
        {
            return FigureType.BISHOP;
        }

        public static void SearchBishopPositions(IBoard board,List<Position> positionList, int x, int y, FigureColor color, int dirX, int dirY)
        {
            if (dirX < -1 || dirX > 1 && dirY < -1 || dirY > 1)
                throw new InvalidOperationException("El valor introducido para dirY o dirX no es validado, introduce 1 o -1 dependiendo de la direccion que desee");

            while(true)
            {
                if (board.CanMove(x + dirX, y + dirY, color) == 0)
                {
                    positionList.Add(new Position(x + dirX, y + dirY));
                    x = x + dirX;
                    y = y + dirY;
                }
                else if (board.CanMove(x + dirX, y + dirY, color) == 1)
                {
                    positionList.Add(new Position(x + dirX, y + dirY));
                    break;
                }
                else if (board.CanMove(x + dirX, y + dirY, color) == -1)
                    break;
            }
        }


        public static List<Position> GetBishopAvaliablePosition(IBoard board, int x, int y, FigureColor color)
        {
            List<Position> positionList = new List<Position>(); 

            SearchBishopPositions(board,positionList, x, y, color, 1, 1);
            SearchBishopPositions(board,positionList, x, y, color, -1, 1);
            SearchBishopPositions(board,positionList, x, y, color, 1, -1);
            SearchBishopPositions(board,positionList, x, y, color, -1, -1);

            return positionList;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetBishopAvaliablePosition(board, X, Y, Color);
        }
    }
}
