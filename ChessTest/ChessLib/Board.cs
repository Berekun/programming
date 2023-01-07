using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public class Board : IBoard
    {
        //Variables
        int _x;
        int _y;
        List<Figure> figures = new List<Figure>();

        //properties
        public int X => _x;
        public int Y => _y;

        //Funciones

        public void GetPawn()
        { 
            for(int i = 0; i <= 8; i++)
            figures.Add(new Pawn(i,2,FigureColor.WHITE));
        }

        public void Move()
        {


        }

        public Figure GetFigureAt(int x, int y)
        {
            return null;
        }
    }
}



