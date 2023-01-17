using System;
using DAM;

namespace TestDAMNuget
{
    public class Sample_FillText : IGameDelegate
    {
        DAM.IFont? font;
        string? posString;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
                Rect2D worldRect = Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0);
                canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
                canvas.Camera.SetRect(worldRect);

            if (posString != null)
            {
                canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(1.0, 0.0);
                canvas.Transform.Rotate(1.0);
                canvas.FillText(new Point2D(0, 0), posString, font, new TextOptions() { height = 0.3, bottomCoords = false });
            }
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            posString = "Hg (" + mouse.X + "," + mouse.Y + ") - (" + pos.x + "," + pos.y + ")";

            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            font = gameEvent.canvasContext.LoadFont("resources/font.json", "resources/font.png");
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}


