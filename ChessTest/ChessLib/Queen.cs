using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Queen : FigureInternal
    {
        public override FigureType GetType()
        {
            return FigureType.QUEEN;
        }
    }
}
