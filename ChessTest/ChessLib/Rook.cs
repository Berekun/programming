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
    }
}
