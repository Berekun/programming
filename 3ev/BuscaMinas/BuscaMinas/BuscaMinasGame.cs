using BuscaMinasLib;
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
        //array de colores
        double random1 = Utils.GetRadomDouble(0, 1);
        double random2 = Utils.GetRadomDouble(0, 1);
        double random3 = Utils.GetRadomDouble(0, 1);

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            canvas.Clear(new rgba_f64(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(rect2d_f64.FromMinMax(-2, -2, _board.GetWidth() + 2, _board.GetHeight() + 2), true);

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
            if (pos.x < 0.0 || pos.y < 0.0)
                return;

            int x = (int)pos.x;
            int y = (int)pos.y;

            if (mouse.IsPressed(MouseButton.Left))
            {
                if (!Utils.IsValueOnRange(x, y, _board))
                    return;

                if (_isLoose)
                    return;

                if (!_firstClick)
                {
                    _board.Init(x, y, 50);
                    OpenSeveralCells(x, y);
                    _firstClick = true;
                }

                if (!_board.IsOpen(x, y))
                {
                    if(_board.GetBombProximity(x,y) == 0)
                        OpenSeveralCells(x, y);
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
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
                {
                    aux = (j + i) % 2 == 0;
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
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
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
                    else if (_board.IsOpen(j, i) && !_board.IsBombAt(j, i) && _board.GetBombProximity(j,i) > 0)
                    {
                        canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 1.0));
                        canvas.Transform.SetTranslation(j + 0.45, i + 0.25);
                        canvas.DrawText(new vec2d_f64(0, 0), $"{_board.GetBombProximity(j, i)}", fontFace, new TextMode() { height = 0.7, bottomCoords = false });
                    }
                    else if (_board.IsBombAt(j, i))
                    {
                        canvas.FillShader.SetColor(new rgba_f64(random1, random2, random3, 1.0));
                        canvas.Transform.SetTranslation(j, i);
                        canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
                    }
                    else
                        continue;
                }
                aux = !aux;
            }
        }

        public void ChangeCellWithBombToOpenCell()
        {
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
                {
                    if(_board.IsBombAt(j,i))
                        _board.OpenCell(j, i);
                }
            }
        }

        public void OpenSeveralCells(int x, int y)
        {
            Position[] positions = new Position[] { new Position(x + 1, y), new Position(x - 1, y), new Position(x, y + 1), new Position(x, y - 1) };

            for(int i = 0; i < 4; i++)
            {
                if (Utils.IsValueOnRange(positions[i].X, positions[i].Y, _board))
                {
                    if (_board.GetBombProximity(positions[i].X, positions[i].Y) == 0 && !_board.IsOpen(positions[i].X, positions[i].Y) && !_board.IsFlagAt(positions[i].X, positions[i].Y) && !_board.IsBombAt(positions[i].X, positions[i].Y))
                    {
                        _board.OpenCell(positions[i].X, positions[i].Y);
                        OpenAroundZero(positions[i].X, positions[i].Y);
                        OpenSeveralCells(positions[i].X, positions[i].Y);
                    }
                }
            }
        }

        public void OpenAroundZero(int x, int y)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!_board.IsOpen(j + x, i + y) && !_board.IsFlagAt(j + x, i + y) && _board.GetBombProximity(j + x,i + y) > 0 && Utils.IsValueOnRange(j + x, i + y, _board))
                        _board.OpenCell(j + x, i + y);
                }
            }
        }
    }
}
