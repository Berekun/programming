using BuscaMinasLib;
using UDK;

namespace BuscaMinasApp
{
    internal class BuscaMinasGame : IGameDelegate
    {
        // Javi: No entiendo esta lista
        private List<IBuffer>? _buffers = new List<IBuffer>();
        private IBoard _board = new BoardArray();
        private bool _firstClick = false;
        private bool _isLoose = false;
        private bool _isWin = false;
        float spawnBombDelay = 0;
        UDK.IFontFace? fontFace;
        rgba_f64[] colors;
        private List<Position>? positionBombs;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Time.UpdateDeltaTime();
            canvas.Clear(new rgba_f64(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(rect2d_f64.FromMinMax(-2, -2, _board.GetWidth() + 2, _board.GetHeight() + 2), true);

            DrawBorad(canvas);
            DrawCells(canvas);

            if (_isLoose && positionBombs == null)
            {
                positionBombs = new List<Position>();
                OpenAllBombs();
                FillListBombs();
            }

            if (_board.HasWin())
                canvas.Clear(new rgba_f64(0.5, 0.8, 0.3, 1.0));
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            if (pos.x < 0.0 || pos.y < 0.0)
                return;

            int x = (int)pos.x;
            int y = (int)pos.y;

            Onclick(gameEvent,keyboard, mouse, x, y);
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            fontFace = gameEvent.canvasContext.CreateFont(CODEC.LoadFontFromFiles("resources/ARCADE_R.ttf"))?.CreateFontFace(64.0);
            _board.CreateBoard(8, 8);
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {

        }

        public void DrawBorad(ICanvas canvas)
        {
            // Javi: Este aux lo manejas mal
            bool aux = true;
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
                {
                    aux = (j + i) % 2 == 0;
                    RenderCellsOrBoard(canvas, j, i, aux, false);
                    aux = !aux;
                }
                aux = !aux;
            }
        }

        public void DrawCells(ICanvas canvas)
        {
            int color = 0;
            bool aux = true;
            spawnBombDelay += Time.deltaTime;

            if (IsTimePassed(spawnBombDelay, 0.2f) && positionBombs != null && positionBombs.Count > 0)
            {
                positionBombs.RemoveAt(0);
                spawnBombDelay = 0;
            }

            // Javi: habría que hacer que este for ocupara menos
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
                {
                    if (_board.IsFlagAt(j, i))
                    {
                        RenderFlags(canvas, j, i);
                    }
                    else if (!_board.IsOpen(j, i))
                    {
                        aux = (i + j) % 2 == 0;
                        RenderCellsOrBoard(canvas, j, i, aux, true);
                        aux = !aux;
                    }
                    else if (_board.IsOpen(j, i) && !_board.IsBombAt(j, i) && _board.GetBombProximity(j, i) > 0)
                    {
                        RenderNumbers(canvas, j, i);
                    }
                    else if (_board.IsBombAt(j, i) && _board.IsOpen(j, i))
                    {
                        if (Contains(j,i,positionBombs))
                        {
                            aux = (i + j) % 2 == 0;
                            RenderCellsOrBoard(canvas, j, i, aux, true);
                            aux = !aux;
                        }
                        else
                        {
                            RenderBombs(canvas, j, i, color);
                            color++;
                        }
                    }
                    else
                        continue;
                }
                aux = !aux;
            }
        }

        public void FillListBombs()
        {
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
                {
                    if (_board.IsBombAt(j, i))
                        positionBombs.Add(new Position(j, i));
                }
            }
        }

        public void OpenAllBombs()
        {
            for (int i = 0; i < _board.GetHeight(); i++)
            {
                for (int j = 0; j < _board.GetWidth(); j++)
                {
                    if (_board.IsBombAt(j, i))
                        _board.OpenCell(j, i);
                }
            }
        }

        // Javi: Un solo return
        public bool IsTimePassed(float spawnBombDelay, float timeTopass)
        {
            if (spawnBombDelay >= timeTopass)
                return true;
            return false;
        }

        public void OpenSeveralCells(int x, int y)
        {
            Position[] positions = new Position[] { new Position(x + 1, y), new Position(x - 1, y), new Position(x, y + 1), new Position(x, y - 1) };

            for (int i = 0; i < 4; i++)
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
                    // Javi: Este if ....
                    if (!_board.IsOpen(j + x, i + y) && !_board.IsFlagAt(j + x, i + y) && _board.GetBombProximity(j + x, i + y) > 0 && Utils.IsValueOnRange(j + x, i + y, _board))
                        _board.OpenCell(j + x, i + y);
                }
            }
        }

        public void CreateColors()
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new rgba_f64(Utils.GetRadomDouble(0, 1), Utils.GetRadomDouble(0, 1), Utils.GetRadomDouble(0, 1), 1.0);
            }
        }

        public void RenderBombs(ICanvas canvas, int x, int y, int color)
        {
            canvas.FillShader.SetColor(colors[color]);
            canvas.Transform.SetTranslation(x, y);
            canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));

            canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 0.6));
            canvas.Transform.SetTranslation(x + 0.25, y + 0.25);
            canvas.Mask.PushCircle(new rect2d_f64(0.0, 0.0, 0.5, 0.5));
            canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 0.5, 0.5));
            canvas.Mask.Pop();
        }

        public void RenderNumbers(ICanvas canvas, int x, int y)
        {
            canvas.FillShader.SetColor(GetColorForNumbers(_board.GetBombProximity(x, y)));
            canvas.Transform.SetTranslation(x + 0.35, y + 0.25);
            canvas.DrawText(new vec2d_f64(0, 0), $"{_board.GetBombProximity(x, y)}", fontFace, new TextMode() { height = 0.4, bottomCoords = false });
        }

        public void RenderFlags(ICanvas canvas, int x, int y)
        {
            canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
            canvas.Transform.SetTranslation(x, y);
            canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
        }

        public void RenderCellsOrBoard(ICanvas canvas, int x, int y, bool aux, bool option)
        {
            if (option)
            {
                if (aux)
                    canvas.FillShader.SetColor(new rgba_f64(0.6, 0.8, 0.5, 1.0));
                else
                    canvas.FillShader.SetColor(new rgba_f64(0.0, 0.56, 0.22, 1.0));
                canvas.Transform.SetTranslation(x, y);
                canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
            }
            else
            {
                if (aux)
                    canvas.FillShader.SetColor(new rgba_f64(0.5, 0.3, 0.1, 1));
                else
                    canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(x, y);
                canvas.DrawRectangle(new rect2d_f64(0, 0, 1.0, 1.0));
            }
        }

        public void Onclick(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse, int x, int y)
        {
            if (mouse.IsPressed(MouseButton.Left))
            {
                if (!Utils.IsValueOnRange(x, y, _board))
                    return;

                if (_isLoose)
                    return;

                if (!_firstClick)
                {
                    _board.Init(x, y, 10);
                    colors = new rgba_f64[_board.BombsCount()];
                    OpenSeveralCells(x, y);
                    _firstClick = true;
                }

                if (!_board.IsOpen(x, y))
                {
                    if (_board.GetBombProximity(x, y) == 0)
                        OpenSeveralCells(x, y);
                    _board.OpenCell(x, y);
                }

                if (_board.IsBombAt(x, y) && _board.IsOpen(x, y))
                {
                    _isLoose = true;
                    CreateColors();
                }
            }

            if (mouse.IsPressed(MouseButton.Right))
            {
                if (!_board.IsOpen(x, y) && !_board.IsFlagAt(x, y))
                    _board.SetFlagAt(x, y);
                else if (_board.IsFlagAt(x, y))
                    _board.DeleteFlagAt(x, y);
            }

            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
        }

        // Javi: Esta función la pondría en Utils
        public rgba_f64 GetColorForNumbers(int number)
        {
            switch (number)
            {
                case 1:
                    return new rgba_f64(0.0, 0.0, 1.0, 1.0);
                case 2:
                    return new rgba_f64(0.0, 1.0, 0.0, 1.0);
                case 3:
                    return new rgba_f64(1.0, 0.0, 0.0, 1.0);
                case 4:
                    return new rgba_f64(0.34, 0.13, 0.39, 1.0);
                case 5:
                    return new rgba_f64(1.0, 0.5, 0.0, 1.0);
                case 6:
                    return new rgba_f64(1.0, 1.0, 0.0, 1.0);
                case 7:
                    return new rgba_f64(0.23, 0.5, 0.74, 1.0);
                case 8:
                    return new rgba_f64(0.0, 0.0, 0.0, 1.0);
                default:
                    return new rgba_f64(0.0, 0.0, 0.0, 0.0);

            }
        }

        // Javi: Esta función ya la tienes hecha
        public bool Contains(int x, int y, List<Position> list)
        {
            if (list == null)
                return false;

            foreach (Position pos in list)
            {
                if (pos.X == x && pos.Y == y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
