namespace ChessLib
{
    abstract class FigureInternal : Figure
    {
        public FigureInternal(int x, int y, FigureColor color) : base(x, y, color)
        {

        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            throw new NotImplementedException();
        }

        public override void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
