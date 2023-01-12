using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    abstract class FigureInternal : Figure
    {
        public FigureInternal(int x, int y, FigureColor color) : base(x, y, color)
        {

        }

        public override List<Position> GetAvaliablePosition()
        {
            throw new NotImplementedException();
        }
        
        public void GetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
