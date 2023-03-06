using System;
using DAM;

namespace TestDAMNuget
{
    public class Sample_FillRect : IGameDelegate
    {
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Rect2D worldRect = Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0);
            canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(worldRect);

            canvas.FillShader.SetColor(new RGBA(0.0, 0.0, 1.0, 1.0));
            canvas.Transform.SetIdentity();
            canvas.FillRectangle(new Rect2D(-2.0, -2.0, 12.0, 4.0));

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetIdentity();
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
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

