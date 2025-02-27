using System.Runtime.InteropServices.ObjectiveC;

namespace BuscaMinasLib
{
    public class Position
    {
        #region ATRIBUTOS
        private int _x, _y;
        #endregion

        #region PROPERTIES
        public int X => _x;

        public int Y => _y;
        #endregion

        #region CONSTRUCTORES
        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public Position()
        {

        }
        #endregion
    }
}
