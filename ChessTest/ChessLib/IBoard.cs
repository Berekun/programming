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

        public bool AntiSuicide(Figure f);

        //Funcion que comprueba las condiciones de limites y movimientos de las figuras
        public int CanMove(int x, int y, FigureColor color)
        {
            Figure figure = GetFigureAt(x, y);
            if (7 < x || x < 0)
                return -1;
            else if (7 < y || y < 0)
                return -1;
            else if (figure != null && figure.Color != color)
                return 1;
            else if(figure != null && figure.Color == color)
                return -1;
            return 0;
        }
    }
}
