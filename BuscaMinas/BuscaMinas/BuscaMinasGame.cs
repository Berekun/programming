using BuscaMinasLib;
using System.Runtime.Intrinsics.X86;
using UDK;

namespace BuscaMinasApp
{
    internal class BuscaMinasGame : IGameDelegate
    {
        private List<IBuffer>? _buffers = new List<IBuffer>();
        private IBoard _board = new Board();
        private bool _firstClick = false;
        private bool _isLoose = false;
        private bool _isWin = false;
        UDK.IFontFace? fontFace;


        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            canvas.Clear(new rgba_f64(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(rect2d_f64.FromMinMax(-2, -2, 10, 10), true);

            DrawBorad(canvas);
            DrawCells(canvas);

            if(_isLoose)
                ChangeCellWithBombToOpenCell();

            if(_board.HasWin())
                canvas.Clear(new rgba_f64(0.5, 0.8, 0.3, 1.0));
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            int x = (int)pos.x;
            int y = (int)pos.y;

            if (mouse.IsPressed(MouseButton.Left))
            {
                if (_isLoose)
                    return;

                if (!_firstClick)
                {
                    _board.Init(x, y, 10);
                    _firstClick = true;
                }

                if (!_board.IsOpen(x, y))
                {
                    _board.OpenCell(x, y);
                }

                if (_board.IsBombAt(x,y) && _board.IsOpen(x,y))
                {
                    _isLoose = true;
                }
            }

            if (mouse.IsPressed(MouseButton.Right))
            {
                if (!_board.IsOpen(x, y) && !_board.IsFlagAt(x,y))
                    _board.SetFlagAt(x, y);
                else if (_board.IsFlagAt(x,y))
                    _board.DeleteFlagAt(x, y);
            }


        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            fontFace = gameEvent.canvasContext.CreateFont(CODEC.LoadFontFromFiles("resources/ArialCE.ttf"))?.CreateFontFace(64.0);

        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
            
        }

        public void DrawBorad(ICanvas canvas)
        {
            _board.CreateBoard(8, 8);
            bool aux = true;
            for (int i = 0; i < _board.GetWidth(); i++)
            {
                for (int j = 0; j < _board.GetHeight(); j++)
                {
                    aux = (i + j) % 2 == 0;
                    if (aux)
                        canvas.FillShader.SetColor(new rgba_f64(0.5, 0.3, 0.1, 1));
                    else
                        canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
                    canvas.Transform.SetTranslation(j, i);
                    canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
                    aux = !aux;
                }
                aux = !aux;
            }
        }

        public void DrawCells(ICanvas canvas)
        {
            bool aux = true;
            for (int i = 0; i < _board.GetWidth(); i++)
            {
                for (int j = 0; j < _board.GetHeight(); j++)
                {
                    if (_board.IsFlagAt(j, i))
                    {
                        canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
                        canvas.Transform.SetTranslation(j, i);
                        canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
                    }
                    else if (!_board.IsOpen(j, i))
                    {
                        aux = (i + j) % 2 == 0;
                        if (aux)
                            canvas.FillShader.SetColor(new rgba_f64(0.6, 0.8, 0.5, 1.0));
                        else
                            canvas.FillShader.SetColor(new rgba_f64(0.0, 0.56, 0.22, 1.0));
                        canvas.Transform.SetTranslation(j, i);
                        canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
                        
                        aux = !aux;
                    }
                    else if (_board.IsOpen(j, i) && !_board.IsBombAt(j, i))
                    {
                        canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 1.0));
                        canvas.Transform.SetTranslation(j + 0.45, i + 0.25);
                        canvas.DrawText(new vec2d_f64(0, 0), $"{_board.GetBombProximity(j, i)}", fontFace, new TextMode() { height = 0.7, bottomCoords = false });
                    }
                    else
                        continue;
                }
                aux = !aux;
            }
        }

        public void ChangeCellWithBombToOpenCell()
        {
            for (int i = 0; i < _board.GetWidth(); i++)
            {
                for (int j = 0; j < _board.GetHeight(); j++)
                {
                    if(_board.IsBombAt(j,i))
                        _board.OpenCell(j, i);
                }
            }
        }
    }
}
