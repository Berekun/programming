using System;
using ChessLib;
using DAM;
using OpenTK.Graphics.ES11;

namespace ChessApp
{
    public class MyChessGame : IGameDelegate
    {
        List<IBuffer>? buffers = new List<IBuffer>();
        Board board = new Board();
        Rect2D worldRect = Rect2D.FromMinMax(0.0, 0.0, 8.0, 8.0);
        List<Position>? positions = null;
        Figure? figure = null;
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            canvas.Clear(new RGBA(0.38, 0.28, 0.15, 1.0));
            canvas.Camera.SetRect(worldRect);

            DrawBorad(canvas);
            DrawFigures(canvas, board, buffers);
            RenderAvaliblePosition(canvas, board, figure);


        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();

            OnClick(gameEvent, mouse);
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            LoadSprites(gameEvent, buffers);
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }

        public void OnClick(GameDelegateEvent gameEvent, IMouse mouse)
        {
            if (mouse.IsPressed(MouseButton.Left))
            {
                float x = mouse.X;
                float y = mouse.Y;
                var position = gameEvent.coordinateConversor.ViewToWorld(x, y);
                if (position.x < 0 || position.x > 8)
                    return;

                if(positions != null)
                {
                    board.Move(figure, (int)position.x, (int)position.y);
                    figure = null;
                    positions = null;
                    return; 
                }
                
                figure = board.GetFigureAt((int)position.x, (int)position.y);
                positions = figure?.GetAvaliablePosition(board);
            }
        }

        //Dibuja el tablero
        public void DrawBorad(ICanvas canvas)
        {
            bool aux = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (aux)
                        canvas.FillShader.SetColor(new RGBA(0.5, 0.3, 0.1, 1));
                    else
                        canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                    canvas.Transform.SetIdentity();
                    canvas.FillRectangle(new Rect2D(j, i, 1.0, 1.0));
                    aux = !aux;
                }
                aux = !aux;
            }
        }

        // Dibuja las figuras
        public void DrawFigures(ICanvas canvas, Board board, List<IBuffer> buffer)
        {
            for(int i = 0; i < board.FigureCount; i++)
            {
                Figure figure = board.GetFigureIndex(i);
                RGBA color = GetImageColor(figure);
                switch (figure.GetType())
                {
                    case FigureType.PAWN:
                        canvas.FillShader.SetImage(buffer[2],color);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.ROOK:
                        canvas.FillShader.SetImage(buffer[0],color);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.KNIGHT:
                        canvas.FillShader.SetImage(buffer[3], color);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.BISHOP:
                        canvas.FillShader.SetImage(buffer[1], color);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.QUEEN:
                        canvas.FillShader.SetImage(buffer[5], color);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.KING:
                        canvas.FillShader.SetImage(buffer[4], color);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    default:
                        break;
                }
            }

        }

        //Carga las figuras
        public void LoadSprites(GameDelegateEvent gameEvent, List<IBuffer>? buffers)
        {
            buffers.Add(IAtomicDecoder.LoadFromFile("resources\\torre.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true));
            buffers.Add(IAtomicDecoder.LoadFromFile("resources\\arfil.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true));
            buffers.Add(IAtomicDecoder.LoadFromFile("resources\\peon.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true));
            buffers.Add(IAtomicDecoder.LoadFromFile("resources\\caballo.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true));
            buffers.Add(IAtomicDecoder.LoadFromFile("resources\\rei.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true));
            buffers.Add(IAtomicDecoder.LoadFromFile("resources\\reina.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true));
        }

        public void RenderAvaliblePosition(ICanvas canvas,IBoard board, Figure? figure)
        {
            if (figure == null)
                return;

            List<Position> list = figure.GetAvaliablePosition(board);
            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 0.0, 0.2));
            canvas.Transform.SetIdentity();
            canvas.FillRectangle(new Rect2D(figure.X, figure.Y, 1, 1));

            foreach(Position position in list)
            {
                canvas.FillShader.SetColor(new RGBA(0.0, 0.0, 0.0, 0.5));
                canvas.Transform.SetTranslation(0.0,0.0);
                canvas.FillRectangle(new Rect2D(position.x + 0.35, position.y + 0.35, 0.25, 0.25));
            }
        }

        private RGBA GetImageColor(Figure? figure)
        {

            if(figure != null && figure.Color == FigureColor.BLACK)
                return new RGBA(0.1, 0.1, 0.1, 1.0);
            return new RGBA(1, 1, 1, 1);
        }
    }
}

