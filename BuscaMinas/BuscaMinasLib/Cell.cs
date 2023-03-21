namespace BuscaMinasLib
{
    internal class Cell
    {
        private bool _isOpen = false;
        private bool _isBomb = false;
        private bool _isFlag = false;

        public bool IsBomb => _isBomb;

        public bool IsFlag => _isFlag;

        public bool IsOpen => _isOpen;

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
    }
}
