namespace BuscaMinasLib
{
    internal class Cell
    {
        #region ATRIBUTOS
        private bool _isOpen = false;
        private bool _isBomb = false;
        private bool _isFlag = false;
        #endregion

        #region PROPERTIES
        public bool IsBomb => _isBomb;

        public bool IsFlag => _isFlag;

        public bool IsOpen => _isOpen;
        #endregion

        #region FUNCIONES_SET
        public void SetBomb(bool option)
        {
            _isBomb = option;
        }

        public void SetFlag(bool option)
        {
            _isFlag = option;
        }

        public void SetCellState(bool option)
        {
            _isOpen = option;
        }
        #endregion
    }
}
