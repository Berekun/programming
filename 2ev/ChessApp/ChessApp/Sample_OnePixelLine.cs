using System;
using DAM;

namespace TestDAMNuget
{
    public class Sample_OnePixelLine : IGameDelegate
    {
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Rect2D worldRect = Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0);
            canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(worldRect);

            {
                var line = canvas.AcquirePath();
                line.MoveTo(new Point2D(0.0, 0.0));
                line.LineTo(new Point2D(1.0, 0.0));
                line.LineTo(new Point2D(1.0, 1.0));
                line.LineTo(new Point2D(0.0, 1.0));
                line.Close();

                canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(0.0, 0.0);
                canvas.StrokeOnePixelLine(line, true);
            }

            {
                var line = canvas.AcquirePath();
                line.MoveTo(new Point2D(0.0, 0.0));
                line.LineTo(new Point2D(0.0, 1.0));
                line.MoveTo(new Point2D(1.0, 0.0));
                line.LineTo(new Point2D(1.0, 1.0));

                canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(2.0, 0.0);
                canvas.StrokeOnePixelLine(line, true);
            }
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}

