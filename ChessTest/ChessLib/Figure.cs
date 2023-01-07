using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public enum FigureType
    {
        BISHOP, KING, QUEEN, PAWN, ROOK, KNIGHT
    }

    public enum FigureColor
    {
        BLACK, WHITE
    }
    public abstract class Figure
    {
        //Variables
        protected int _x, _y;
        protected FigureColor _color;

        //Properties
        public int X => _x;

        public int Y => _y;

        public FigureColor Color => _color;


        //Funciones
        public Figure()
        {

        }
        
        public Figure(int x, int y, FigureColor color)
        {
            _x = x;
            _y = y;
            _color = color;
        }

    }

}
