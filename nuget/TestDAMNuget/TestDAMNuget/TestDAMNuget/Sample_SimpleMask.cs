using System;
using DAM;

namespace TestDAMNuget
{
    public class Sample_SimpleMask : IGameDelegate
    {
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Rect2D worldRect = Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0);
            canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(worldRect);

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(0.0, 1.0);
            canvas.Mask.Push(true).SetRoundedRect(new Rect2D(0.0, 0.0, 1.0, 1.0), 0.2f, 0.3f, 0.4f, 0.5f);
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.Mask.Pop();

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(1.0, 1.0);
            canvas.Mask.Push(true).SetLineWidth(0.02).SetRoundedRect(new Rect2D(0.0, 0.0, 1.0, 1.0), 0.2f, 0.3f, 0.4f, 0.5f);
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.Mask.Pop();

            canvas.FillShader.SetRadialGradient(new Rect2D(0.0, 0.0, 1.0, 1.0), new RGBA(1.0, 0.0, 0.0, 1.0), new RGBA(0.0, 1.0, 0.0, 1.0));
            canvas.Transform.SetTranslation(1.0f, 0.0f);
            canvas.Mask.Push(true).SetCircle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.Mask.Pop();
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

