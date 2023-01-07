using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public interface IBoard
    {
        Figure GetFigureAt(int x, int y);
    }
}
