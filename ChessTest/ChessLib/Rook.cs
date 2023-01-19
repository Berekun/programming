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

        public static List<Position> GetRookAvaliablePosition(IBoard board, int startx,int starty, FigureColor color) //CAMBIAR, HAY QUE HACER UNA FUNCIUON CON UN FOR Y DEPENDE EL VALOR QUE LE PASES HACE UNA DIRECCION U OTRA
        {
            List<Position> positionList = new List<Position>();

            for(int x = 0; x < 8; x++)
            {
                if (board.CanMove(x, starty, color) == 0)
                    positionList.Add(new Position(x, starty));
                else if(board.CanMove(x, starty, color) == 1)
                {
                    positionList.Add(new Position(x, starty));
                    break;
                }
            }

            for (int x = 7; x >= 0; x--)
            {
                if (board.CanMove(x, starty, color) == 0)
                    positionList.Add(new Position(x, starty));
                else if (board.CanMove(x, starty, color) == 1)
                {
                    positionList.Add(new Position(x, starty));
                    break;
                }
            }

            for (int y = 0; y < 8; y++)
            {
                if (board.CanMove(y, starty, color) == 0)
                    positionList.Add(new Position(y, startx));
                else if (board.CanMove(y, starty, color) == 1)
                {
                    positionList.Add(new Position(y, startx));
                    break;
                }
            }

            for (int y = 7; y >= 0; y--)
            {
                if (board.CanMove(y, starty, color) == 0)
                    positionList.Add(new Position(y, startx));
                else if (board.CanMove(y, starty, color) == 1)
                {
                    positionList.Add(new Position(y, startx));
                    break;
                }
            }

            return positionList;
        }

        /*public override List<Position> GetAvaliablePosition(IBoard board)
        {
            return GetRookAvaliablePosition();
        }*/
    }
}
