using System;
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

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            List<Position> positionsList = new List<Position>();

            SearchPawnPosition(board, positionsList, Y, Color);

            return positionsList;
        }

        public void SearchPawnPosition(IBoard board, List<Position> list, int starty, FigureColor color)
        {
            int dir = 0;
            if (color == FigureColor.WHITE)
                dir++;
            else
                dir--;

            if(color == FigureColor.WHITE && starty == 1 && board.CanMove(X,Y + 2,  color) == 0 && board.CanMove(X, Y + 1, color) == 0)
                list.Add(new Position(X, Y + dir * 2));
            if(color == FigureColor.BLACK && starty == 6 && board.CanMove(X, Y - 2, color) == 0 && board.CanMove(X, Y - 1, color) == 0)
                list.Add(new Position(X, Y + dir * 2));

            // Javi: Quizás los hubiese puesto todos en el mismo if
            if(board.CanMove(X,Y + dir,color) == 0)
                list.Add(new Position(X, Y + dir));

            if (board.CanMove(X + 1, Y + dir, color) == 1)
                list.Add(new Position(X + 1, Y + dir));

            if (board.CanMove(X - 1, Y + dir, color) == 1)
                list.Add(new Position(X - 1, Y + dir));
        }
    }
}
