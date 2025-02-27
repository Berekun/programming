﻿using System.Drawing;

namespace ChessLib
{
    public class Board : IBoard
    {
        //Variables
        // Javi: El tablero tiene una xy una y!?!??!!?
        private int _x;
        private int _y;
        private List<Figure> figures = new List<Figure>();
        private int ColorTurn = 0;

        //properties
        public int X => _x;
        public int Y => _y;

        public int FigureCount => figures.Count;

        public List<Figure> FigureList => figures;

        //Funciones

        public Board()
        {
            CreateFigures();
        }

        //Crea todas las figuras

        public void CreateFigures()
        {
            CreateMonarchs();
            CreatePawns();
            CreateRooks();
            CreateKnights();
            CreateBishops();
        }

        //Crea los peones

        public void CreatePawns()
        { 
            for(int i = 0; i < 8; i++)
            {
                figures.Add(new Pawn(i, 1, FigureColor.WHITE));
            }
            for (int i = 0; i < 8; i++)
            {
                figures.Add(new Pawn(i, 6, FigureColor.BLACK));
            }
        }

        //Crea las torres

        public void CreateRooks()
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

        public void CreateKnights()
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

        public void CreateBishops()
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
            figures.Add(new King(4, 0, FigureColor.WHITE));
            figures.Add(new King(4, 7, FigureColor.BLACK));
            figures.Add(new Queen(3, 0, FigureColor.WHITE));
            figures.Add(new Queen(3, 7, FigureColor.BLACK));
        }

        //Busca en la lista de figuras una figura y te delvuelvo su posicion en la lista
        public Figure? GetFigureIndex(int index)
        {
            return figures[index];
        }

        //Te dice si en la posicion que le pasas hay o no una figura
        public Figure? GetFigureAt(int x, int y)
        {
            foreach (Figure f in figures)
                if (x == f.X && y == f.Y)
                    return f;
            return null;

        }

        //Verifica si la posicion seleccionada es igual al de una figura para poder moverla
        public void Move(Figure figure, int x, int y)
        {
            if (figure == null)
                return;

            List<Position> positions = figure.GetAvaliablePosition(this);

            foreach(Position position in positions)
            {
                // Javi: Tabula
                if(position.x == x && position.y ==  y)
                Movement(figure, x , y);
            }

        }

        //Mueve la figura y elemina al enemigo en caso de ser comido
        // Javi: Nombre no muy bueno
        public void Movement(Figure figure, int x, int y)
        {
            Figure? enemy = GetFigureAt(x, y);
            figure.SetPosition(x, y);
            ColorTurn++;

            if (enemy != null)
                figures.Remove(enemy);
        }

        //Te duelve un color dependiendo el turno
        // Javi: mal nombre
        public FigureColor GetColorTurn()
        {
            if (ColorTurn % 2 == 0)
                return FigureColor.WHITE;
            return FigureColor.BLACK;
            
        }

        // Javi: ::::DDDDDDD
        public bool AntiSuicide(Figure f,int x,int y)
        {
            Position aux = f.Position;
            f.SetPosition(x, y);
            List<Position> positionList = new List<Position>();

            for (int j = 0; j < FigureCount; j++)
            {
                if (figures[j].Color != f.Color && figures[j].GetType() != FigureType.KING)
                    positionList = figures[j].GetAvaliablePosition(this);

                for (int k = 0; k < positionList.Count; k++)
                {
                    if (f.Position == positionList[k])
                    {
                        f.SetPosition(aux.x, aux.y);
                        return true;
                    }
                }
            }
            f.SetPosition(aux.x, aux.y);
            return false;
        }
    }
}



