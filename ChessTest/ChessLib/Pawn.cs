using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Pawn : FigureInternal
    {
        public override FigureType GetType()
        {
            return FigureType.PAWN;
        }
    }
}
