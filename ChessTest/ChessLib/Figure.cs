using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public enum FigureType
    {
        BISHOP, KING, QUEEN, PAWN, ROCK, KNIGHT
    }

    public enum FigureColor
    {
        BLACK, WHITE
    }
    internal class Figure
    {
        private int _x, _y;
        private FigureColor _color;
        private FigureType _type;
    } 


}
