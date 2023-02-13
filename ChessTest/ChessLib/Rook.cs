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

        public static void SearchRookPositions(IBoard board,List<Position> positionList,int x, int y,FigureColor color,int dirX, int dirY)
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

        public static List<Position> GetRookAvaliablePosition(IBoard board, int x,int y, FigureColor color) //CAMBIAR, HAY QUE HACER UNA FUNCIUON CON UN FOR Y DEPENDE EL VALOR QUE LE PASES HACE UNA DIRECCION U OTRA
        {
            List<Position> positionList = new List<Position>();

            SearchRookPositions(board, positionList, x, y, color, -1, 0);
            SearchRookPositions(board, positionList, x, y, color, 1, 0);
            SearchRookPositions(board, positionList, x, y, color, 0, 1);
            SearchRookPositions(board, positionList, x, y, color, 0, -1);

            return positionList;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetRookAvaliablePosition(board,X,Y,Color);
        }
    }
}
