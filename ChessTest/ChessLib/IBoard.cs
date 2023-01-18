using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public interface IBoard
    {
        Figure GetFigureAt(int x, int y);

        public bool CanMove(int x, int y, FigureColor color)
        {
            Figure figure = GetFigureAt(x, y);
            if (x < 8 && 0 < x && figure == null)
                return true;
            else if (y < 8 && 0 < x && figure == null)
                return true;
            else if (figure != null && figure.Color != color)
                return true;
            return false;
        }
    }
}
