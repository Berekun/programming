using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    abstract class FigureInternal : Figure
    {
        public abstract FigureType GetType();     
        
        public void Position(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
