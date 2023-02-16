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

    public struct Position
    {
        public int x;
        public int y;

        public Position()
        {

        }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return (pos1.x == pos2.x && pos1.y == pos2.y);
        }

        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1 == pos2);
        }


    }
    public abstract class Figure
    {
        //Variables
        protected int _x, _y;
        protected FigureColor _color;
        protected int TotalMoves = 0;

        //Properties
        public int X => _x;

        public int Y => _y;

        public FigureColor Color => _color;

        public Position Position => new(_x, _y);


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

        public abstract FigureType GetType();

        public abstract List<Position> GetAvaliablePosition(IBoard board);

        public abstract void SetPosition(int x, int y);
    }

}
