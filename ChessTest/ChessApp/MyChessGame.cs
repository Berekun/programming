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
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            canvas.Clear(new RGBA(0.38, 0.28, 0.15, 1.0));
            canvas.Camera.SetRect(worldRect);

            DrawBorad(canvas);
            DrawFigures(canvas, board, buffers); 


        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();

            if (mouse.IsPressed(MouseButton.Left))
            {
                float x = mouse.X;
                float y = mouse.Y;
                var position = gameEvent.coordinateConversor.ViewToWorld(x, y);
                board.GetFigureAt((int)position.x, (int)position.y);
            }
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            LoadSprites(gameEvent, buffers);
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
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
                        canvas.FillShader.SetColor(new RGBA(0.2, 0.2, 0.2, 1));
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
                switch (figure.GetType())
                {
                    case FigureType.PAWN:
                        canvas.FillShader.SetImage(buffer[2]);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.ROOK:
                        canvas.FillShader.SetImage(buffer[0]);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.KNIGHT:
                        canvas.FillShader.SetImage(buffer[3]);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.BISHOP:
                        canvas.FillShader.SetImage(buffer[1]);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.QUEEN:
                        canvas.FillShader.SetImage(buffer[5]);
                        canvas.Transform.SetTranslation(figure.X, figure.Y);
                        canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                        break;
                    case FigureType.KING:
                        canvas.FillShader.SetImage(buffer[4]);
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
    }
}

