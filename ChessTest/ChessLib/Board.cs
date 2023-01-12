namespace ChessLib
{
    public class Board : IBoard
    {
        //Variables
        private int _x;
        private int _y;
        private List<Figure> figures = new List<Figure>();

        //properties
        public int X => _x;
        public int Y => _y;

        //Funciones

        //Crea todas las figuras

        public void CreateFigures()
        {
            CreatePawn();
            CreateRook();
            CreateKnight();
            CreateBishop();
            CreateMonarchs();
        }

        //Crea los peones

        public void CreatePawn()
        { 
            for(int i = 0; i < 7; i++)
            {
                figures.Add(new Pawn(i, 1, FigureColor.WHITE));
            }
            for (int i = 0; i < 7; i++)
            {
                figures.Add(new Pawn(i, 6, FigureColor.BLACK));
            }
        }

        //Crea las torres

        public void CreateRook()
        {
            for(int i=0;i <=7; i += 7)
            {
                figures.Add(new Rook(i,0,FigureColor.WHITE));
            }

            for (int i = 0; i <= 7; i += 7)
            {
                figures.Add(new Rook(i, 7, FigureColor.BLACK));
            }
        }

        //Crea los caballos

        public void CreateKnight()
        {
            for (int i = 1; i <= 6; i += 5)
            {
                figures.Add(new Knight(i, 0, FigureColor.WHITE));
            }
            for (int i = 1; i <= 6; i += 5)
            {
                figures.Add(new Knight(i, 7, FigureColor.BLACK));
            }
        }

        //Crea los alfiles

        public void CreateBishop()
        {
            for (int i = 2; i <= 5; i += 3)
            {
                figures.Add(new Bishop(i, 0, FigureColor.WHITE));
            }
            for (int i = 2; i <= 5; i += 3)
            {
                figures.Add(new Bishop(i, 7, FigureColor.BLACK));
            }
        }

        //Crea las reinas y los reyes

        public void CreateMonarchs()
        {
            figures.Add(new Queen(3, 0, FigureColor.WHITE));
            figures.Add(new Queen(3, 7, FigureColor.BLACK));
            figures.Add(new King(4, 0, FigureColor.WHITE));
            figures.Add(new King(4, 7, FigureColor.BLACK));
        }

        public Figure GetFigureAt(int x, int y)
        {
            foreach (Figure f in figures)
                if (x == f.X && y == f.Y)
                    return f;
            return null;

        }
    }
}



